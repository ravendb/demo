import { UserProgress, DemoProgress } from "../../models/progress";
import { DemoStorage } from "./DemoStorage";
import { DemoVersionInfo } from "../../store/selectors/demos";
import { MainPageCategoryDto } from "../../models/dtos";

export class Progress {
    static get(): UserProgress {
        return DemoStorage.getUserProgress();
    }

    static save(versionInfo: DemoVersionInfo) {
        const { category, demo, demoHash } = versionInfo;

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

    private static isUpToDate(progress: DemoProgress, categories: MainPageCategoryDto[]): boolean {
        const currentCategory = categories.find(c => c.slug === progress.category);
        const currentDemo = currentCategory && currentCategory.demos && currentCategory.demos.find(d => d.slug === progress.demo);
        return currentDemo && currentDemo.hash === progress.demoHash;
    }

    static updateDemoVersions(categories: MainPageCategoryDto[]) {
        let progress = DemoStorage.getUserProgress();

        if (!progress || !progress.completedDemos) {
            return;
        }

        progress.completedDemos = progress.completedDemos.filter(x => this.isUpToDate(x, categories));
        DemoStorage.setUserProgress(progress);
    }
}