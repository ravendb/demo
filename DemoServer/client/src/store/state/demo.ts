import { Language } from "../../models/common";
import { DemoEntry } from "./models";
import { UserProgress } from "../../models/progress";
import { CategoryWithDemoVersions, CategoriesForLanguage } from "../../models/dtos/context";
import { CategorySlug, DemoSlug } from "../../models/slugs";

export const defaultLanguage: Language = "csharp";

interface SlugsState {
    categorySlug: CategorySlug;
    demoSlug: DemoSlug;
    wtSlug?: string;
}

interface ProgressState {
    userProgress: UserProgress;
    categoriesWithVersions: CategoryWithDemoVersions[];
}

interface ContextState {
    categoriesForLanguages: CategoriesForLanguage[];
    conferenceMode: boolean;
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
    & ContextState
    & SlugsState
    & {
        language: Language;
        demo: DemoEntry;
        finishedLoadingDemo: boolean;
        showShareMessage: boolean;
        loadingContext: boolean;
    };
