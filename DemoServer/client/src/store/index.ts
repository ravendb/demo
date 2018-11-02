import { createStore, applyMiddleware } from "redux";
import { ThunkDispatch } from "redux-thunk";
import { AppState } from "./state";
import reducer from "./reducers";
import thunk, { ThunkMiddleware } from "redux-thunk";
import { connectRouter, routerMiddleware } from "connected-react-router";
import { createBrowserHistory } from "history";
import { Action } from "./actions";

export const history = createBrowserHistory();

const initialState = {
};

export const store = createStore<AppState, Action, any, any>(
    connectRouter(history)(reducer),
    initialState,
    applyMiddleware(
        routerMiddleware(history),
        thunk as ThunkMiddleware<AppState, Action>
    )
);

export type DemoThunkDispatch = ThunkDispatch<AppState, null, Action>;
export type GetState = () => AppState;