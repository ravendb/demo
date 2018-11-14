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

export function getCurrentWalkthroughIndex(state: DemoState): number {
    const slug = state.currentWalkthroughSlug;
    const walkthroughs = slug && state.demo && state.demo.walkthroughs && state.demo.walkthroughs;
    return walkthroughs ? walkthroughs.findIndex(x => x.slug === slug) : -1;
}

export function getCurrentWalkthrough(state: DemoState): DemoWalkthroughDto {
    const i = getCurrentWalkthroughIndex(state);
    return i === -1 ? null : state.demo.walkthroughs[i];
}