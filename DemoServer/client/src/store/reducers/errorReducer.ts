import { modifyState } from "../state";
import { ErrorAction } from "../actions/errorActions";
import { ErrorState } from "../state/ErrorState";

export function errorReducer(state: ErrorState = { error: null }, action: ErrorAction): ErrorState {
    switch (action.type) {
        case "API_ERROR":
            alert("API error: " + action.error);
            console.log(action.error);
            return modifyState(state, s => s.error = action.error);
    }
    return state;
}