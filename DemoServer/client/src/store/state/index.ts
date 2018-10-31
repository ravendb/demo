import { deepCopy } from "../../utils/miscUtils";
import { ErrorState } from "./ErrorState";
import { DemoState } from "./DemoState";

export interface AppState {
    error: ErrorState;
    demos: DemoState;
}

export function modifyState<S>(state: S, modify: (state: S) => void): S {
    const stateCopy = deepCopy(state);
    modify(stateCopy);
    return stateCopy;
}