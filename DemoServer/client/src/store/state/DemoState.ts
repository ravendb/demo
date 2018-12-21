import { Language } from "../../models/commonModels";
import { DemoEntry } from "./models";

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
    & {
        language: Language;
        categorySlug: string;
        demoSlug: string;
        demo: DemoEntry;
        finishedLoadingDemo: boolean;
        showShareMessage: boolean;
    };
