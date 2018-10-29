import { createStore, applyMiddleware } from "redux";
import { AppState } from "./state";
import { Action } from "../actions";
import reducer from "../reducers";
import thunk, { ThunkMiddleware } from "redux-thunk";

export const store = createStore<AppState, Action, any, any>(reducer, {
    example: {
        loading: false,
        text: null
    }
},
    applyMiddleware(thunk as ThunkMiddleware<AppState, Action>)
);