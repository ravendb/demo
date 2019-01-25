import { modifyState } from "../state";
import { ErrorAction } from "../actions/error";
import { ErrorState } from "../state/error";

export function errorReducer(state: ErrorState = { error: null }, action: ErrorAction): ErrorState {
    switch (action.type) {
        case "API_ERROR":
            console.log(action.error);
            return modifyState(state, s => s.error = action.error);

        case "API_ERROR_RELOAD":
            location.reload();
            return modifyState(state, s => s.error = null);
    }
    return state;
}
