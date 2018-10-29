import { deepCopy } from "../utils/miscUtils";
import { DemoDto } from "../models/dtos";

export interface ErrorState {
    error: any;
}

export interface ExampleState {
    loading: boolean;
    text: string;
}

export interface DemoState {
    demo: DemoDto;
    loadingDemo: boolean;
}

export interface AppState {
    example: ExampleState;
    error: ErrorState;
    demo: DemoState;
}

export function modifyState<S>(state: S, modify: (state: S) => void): S {
    const stateCopy = deepCopy(state);
    modify(stateCopy);
    return stateCopy;
}