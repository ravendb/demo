import { createStore, applyMiddleware } from "redux";
import { AppState } from "./state";
import { DemoAction } from "../actions";
import reducer from "../reducers";
import thunk, { ThunkMiddleware } from "redux-thunk";

export const store = createStore<AppState, DemoAction, any, any>(reducer, {
    example: {
        loading: false,
        text: null
    }
},
    applyMiddleware(thunk as ThunkMiddleware<AppState, DemoAction>)
);