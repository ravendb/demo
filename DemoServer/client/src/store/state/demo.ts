import { Language } from "../../models/common";
import { DemoEntry } from "./models";
import { UserProgress } from "../../models/progress";
import { CategoryHeaderDto } from "../../models/dtos/context";
import { CategorySlug, DemoSlug } from "../../models/slugs";

interface SlugsState {
    categorySlug: CategorySlug;
    demoSlug: DemoSlug;
    wtSlug?: string;
}

interface ProgressState {
    userProgress: UserProgress;
    categories: CategoryHeaderDto[];
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
        loadingContext: boolean;
        conferenceMode: boolean;
    };
