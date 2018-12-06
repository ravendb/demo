import { Language } from "../../models/commonModels";
import { DemoDto, DemoWalkthroughDto } from "../../models/dtos";
import { ParameterPair } from "../../models/demoModels";

interface ParametersState {
    parameters: ParameterPair[];
}

interface PrerequisitesState {
    finishedSettingPrerequisites: boolean;
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
        finishedLoadingDemo: boolean;
        showShareMessage: boolean;
    }

export function getCurrentWalkthroughIndex(state: DemoState): number {
    const slug = state.currentWalkthroughSlug;
    const walkthroughs = slug && state.demo && state.demo.walkthroughs;
    return walkthroughs ? walkthroughs.findIndex(x => x.slug === slug) : -1;
}

export function getWalkthroughsCount(state: DemoState) {
    const wts = state.demo && state.demo.walkthroughs;
    return wts ? wts.length : 0;
}

export function getCurrentWalkthrough(state: DemoState): DemoWalkthroughDto {
    const i = getCurrentWalkthroughIndex(state);
    return i === -1 ? null : state.demo.walkthroughs[i];
}