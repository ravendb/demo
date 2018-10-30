import { createStore, applyMiddleware } from "redux";
import { AppState } from "./state";
import { Action } from "../actions";
import reducer from "../reducers";
import thunk, { ThunkMiddleware } from "redux-thunk";
import { connectRouter, routerMiddleware } from "connected-react-router";
import { createBrowserHistory } from "history";

export const history = createBrowserHistory();

const initialState = {
    example: {
        loading: false,
        text: null
    }
};

export const store = createStore<AppState, Action, any, any>(
    connectRouter(history)(reducer),
    initialState,
    applyMiddleware(
        routerMiddleware(history),
        thunk as ThunkMiddleware<AppState, Action>
    )
);