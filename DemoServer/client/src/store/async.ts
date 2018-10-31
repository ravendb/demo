import { AppState } from "./state";
import { ThunkDispatch } from "redux-thunk";
import { Action } from "./actions";

export type DemoAsyncDispatch = ThunkDispatch<AppState, null, Action>;
export type GetState = () => AppState;