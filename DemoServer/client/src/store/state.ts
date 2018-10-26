import { deepCopy } from "../utils/miscUtils";

export interface ErrorState {
    error: any;
}

export interface ExampleState {
    loading: boolean;
    text: string;
}

export interface AppState {
    example: ExampleState;
    error: ErrorState;
}

export function modifyState<S>(state: S, modify: (state: S) => void): S {
    const stateCopy = deepCopy(state);
    modify(stateCopy);
    return stateCopy;
}