import { Language } from "../../models/commonModels";
import { DemoEntry } from "./models";
import { UserProgress } from "../../models/progress";
import { DemoVersionDto } from "../../models/dtos";

interface SlugsState {
    categorySlug: string;
    demoSlug: string;
    wtSlug?: string;
}

interface ProgressState {
    userProgress: UserProgress;
    demoVersions: DemoVersionDto[];
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
    };
