import { ErrorState, modifyState } from "../store/state";
import { ErrorAction } from "../actions/errorActions";

export function errorReducer(state: ErrorState = { error: null }, action: ErrorAction): ErrorState {
    switch (action.type) {
        case "API_ERROR":
            alert("API error: " + action.error);
            console.log(action.error);
            return modifyState(state, s => s.error = action.error);
    }
    return state;
}