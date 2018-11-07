import { Language } from "../../models/commonModels";
import { DemoDto, DemoWalkthroughDto } from "../../models/dtos";
import { ParameterPair } from "../../models/demoModels";

interface ParametersState {
    parameters: ParameterPair[];
}

interface PrerequisitesState {
    settingPrerequisites: boolean;
}

interface RunResultsState {
    showResultsPanel: boolean;
    loadingRunResults: boolean;
    runResults: any;
}

interface WalkthroughState {
    currentWalkthroughSlug?: string;
}

export function getCurrentWalkthrough(state: DemoState): DemoWalkthroughDto {
    const slug = state.currentWalkthroughSlug;
    return slug && state.demo && state.demo.walkthroughs
        && state.demo.walkthroughs.find(x => x.slug === slug);
}

export type DemoState = ParametersState
    & RunResultsState
    & WalkthroughState
    & PrerequisitesState
    & {
        language: Language;
        categorySlug: string;
        demoSlug: string;
        demo: DemoDto;
        loadingDemo: boolean;
    }