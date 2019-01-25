import { UserProgress, DemoProgress } from "../../models/progress";
import { DemoStorage } from "./DemoStorage";
import { DemoVersionInfo } from "../../store/selectors/demos";
import { CategoryHeaderDto } from "../../models/dtos/context";

export class Progress {
    public static get(): UserProgress {
        return DemoStorage.getUserProgress();
    }

    private static _getEmptyProgress(): UserProgress {
        return {
            completedDemos: []
        };
    }

    public static save(versionInfo: DemoVersionInfo) {
        const { category, demo, demoHash } = versionInfo;

        let progress = DemoStorage.getUserProgress();

        if (!progress) {
            progress = this._getEmptyProgress();
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

    public static reset() {
        const progress = this._getEmptyProgress();
        DemoStorage.setUserProgress(progress);
    }

    private static _isUpToDate(progress: DemoProgress, categories: CategoryHeaderDto[]): boolean {
        const currentCategory = categories.find(c => c.slug === progress.category);

        const currentDemo = currentCategory
            && currentCategory.demos
            && currentCategory.demos.find(d => d.slug === progress.demo);

        return currentDemo && currentDemo.hash === progress.demoHash;
    }

    public static updateDemoVersions(categories: CategoryHeaderDto[]) {
        const progress = DemoStorage.getUserProgress();

        if (!progress || !progress.completedDemos) {
            return;
        }

        progress.completedDemos = progress.completedDemos.filter(x => this._isUpToDate(x, categories));
        DemoStorage.setUserProgress(progress);
    }
}
