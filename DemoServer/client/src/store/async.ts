import { Action } from "../actions";
import { AppState } from "./state";
import { ThunkDispatch } from "redux-thunk";

export type DemoAsyncDispatch = ThunkDispatch<AppState, null, Action>;
export type GetState = () => AppState;