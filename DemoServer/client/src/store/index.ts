import { createStore, applyMiddleware } from "redux";
import { composeWithDevTools } from "redux-devtools-extension";
import { ThunkDispatch } from "redux-thunk";
import { AppState } from "./state";
import createRootReducer from "./reducers";
import thunk, { ThunkMiddleware } from "redux-thunk";
import { routerMiddleware } from "connected-react-router";
import { createBrowserHistory } from "history";
import { Action } from "./actions";

export const history = createBrowserHistory();

const initialState = {
};

export const store = createStore(
    createRootReducer(history),
    initialState,
    composeWithDevTools(
        applyMiddleware(
            routerMiddleware(history),
            thunk as ThunkMiddleware<AppState, Action>
        )
    ));

export type DemoThunkDispatch = ThunkDispatch<AppState, null, Action>;
export type GetState = () => AppState;