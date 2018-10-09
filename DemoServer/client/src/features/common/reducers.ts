import { modifyState, ErrorState } from "../../store/state";
import { ErrorAction } from "./actions";
import { API_ERROR } from "./actionNames";

export function errorReducer(state: ErrorState = { error: null }, action: ErrorAction): ErrorState {
    switch (action.type) {
        case API_ERROR:
            alert("API error: " + action.error);
            return modifyState(state, s => s.error = action.error);
    }
    return state;
}