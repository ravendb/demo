import { deepCopy } from "../utils/miscUtils";
import { DemoDto } from "../models/dtos";
import { Language } from "../models/commonModels";
import { ParameterPair } from "../models/demoModels";

export interface ErrorState {
    error: any;
}

export interface DemoState {
    language: Language;
    categorySlug: string;
    demoSlug: string;
    demo: DemoDto;
    loadingDemo: boolean;
    loadingRunResults: boolean;
    parameters: ParameterPair[];
    runResults: any;
}

export interface AppState {
    error: ErrorState;
    demos: DemoState;
}

export function modifyState<S>(state: S, modify: (state: S) => void): S {
    const stateCopy = deepCopy(state);
    modify(stateCopy);
    return stateCopy;
}