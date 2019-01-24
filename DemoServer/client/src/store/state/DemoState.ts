import { Language } from "../../models/common";
import { DemoEntry } from "./models";
import { UserProgress } from "../../models/progress";
import { MainPageCategoryDto } from "../../models/dtos";
import { CategorySlug, DemoSlug } from "../../models/slugs";

interface SlugsState {
    categorySlug: CategorySlug;
    demoSlug: DemoSlug;
    wtSlug?: string;
}

interface ProgressState {
    userProgress: UserProgress;
    categories: MainPageCategoryDto[];
}

interface PrerequisitesState {
    finishedSettingPrerequisites: boolean;
}

interface RunResultsState {
    showResultsPanel: boolean;
    loadingRunResults: boolean;
    runResults: any;
}

export type DemoState = RunResultsState
    & PrerequisitesState
    & ProgressState
    & SlugsState
    & {
        language: Language;
        demo: DemoEntry;
        finishedLoadingDemo: boolean;
        showShareMessage: boolean;
        loadingMainPage: boolean;
        conferenceMode: boolean;
    };
