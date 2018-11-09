import { UserProgress } from "../../models/progress";
import { DemoStorage } from "./DemoStorage";

export class Progress {
    static get(): UserProgress {
        return DemoStorage.getUserProgress();
    }

    static save(category: string, demo: string) {
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
            progress.completedDemos.push({ category, demo });
        }

        DemoStorage.setUserProgress(progress);
    }
}