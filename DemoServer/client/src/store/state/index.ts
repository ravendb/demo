import { deepCopy } from "../../utils/miscUtils";
import { ErrorState } from "./ErrorState";
import { DemoState } from "./DemoState";
import { ProgressState } from "./ProgressState";
import { ParametersState } from "./ParametersState";

export interface AppState {
    error: ErrorState;
    demos: DemoState;
    params: ParametersState;
    progress: ProgressState;
}

export function modifyState<S>(state: S, modify: (state: S) => void): S {
    const stateCopy = deepCopy(state);
    modify(stateCopy);
    return stateCopy;
}