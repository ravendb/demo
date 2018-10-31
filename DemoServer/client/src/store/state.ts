import { deepCopy } from "../utils/miscUtils";
import { DemoDto } from "../models/dtos";
import { Language } from "../models/commonModels";
import { RouterState } from "react-router-redux";
import { ParameterPair } from "../models/demoModels";

export interface ErrorState {
    error: any;
}

export interface ExampleState {
    loading: boolean;
    text: string;
}

export interface DemoState {
    language: Language;
    categorySlug: string;
    demoSlug: string;
    demo: DemoDto;
    loadingDemo: boolean;
    loadingRunResults: boolean;
    parameters: ParameterPair[];
    runResults: object;
}

export interface AppState {
    example: ExampleState;
    error: ErrorState;
    demos: DemoState;
    routing: RouterState;
}

export function modifyState<S>(state: S, modify: (state: S) => void): S {
    const stateCopy = deepCopy(state);
    modify(stateCopy);
    return stateCopy;
}