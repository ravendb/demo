import { deepCopy } from "../../utils/miscUtils";
import { RouterState } from "connected-react-router";
import { ErrorState } from "./ErrorState";
import { DemoState } from "./DemoState";
import { ParametersState } from "./ParametersState";

export interface AppState {
    router: RouterState;
    error: ErrorState;
    demos: DemoState;
    params: ParametersState;
}

export function modifyState<S>(state: S, modify: (state: S) => void): S {
    const stateCopy = deepCopy(state);
    modify(stateCopy);
    return stateCopy;
}
