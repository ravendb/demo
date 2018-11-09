import { UserProgress, DemoProgress } from "../../models/progress";
import { DemoStorage } from "./DemoStorage";
import { DemoVersionDto } from "../../models/dtos";

export class Progress {
    static get(): UserProgress {
        return DemoStorage.getUserProgress();
    }

    static save(category: string, demo: string, demoHash: string) {
        let progress = DemoStorage.getUserProgress();

        if (!progress) {
            progress = {
                completedDemos: []
            };
        }

        if (!progress.completedDemos) {
            progress.completedDemos = [];
        }

        const existingEntry = progress.completedDemos.find(x => x.category === category && x.demo === demo);
        if (!existingEntry) {
            progress.completedDemos.push({ category, demo, demoHash });
        }

        DemoStorage.setUserProgress(progress);
    }

    private static isUpToDate(progress: DemoProgress, currentVersions: DemoVersionDto[]): boolean {
        const current = currentVersions.find(c => c.category === progress.category && c.demo === progress.demo);
        return current && current.hash === progress.demoHash;
    }

    static updateDemoVersions(currentVersions: DemoVersionDto[]) {
        let progress = DemoStorage.getUserProgress();

        if (!progress || !progress.completedDemos) {
            return;
        }

        progress.completedDemos = progress.completedDemos.filter(x => this.isUpToDate(x, currentVersions));
        DemoStorage.setUserProgress(progress);
    }
}