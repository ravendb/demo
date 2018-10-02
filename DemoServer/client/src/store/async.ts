import { DemoAction } from "./actions";
import { AppState } from "./state";
import { ThunkAction, ThunkDispatch } from "redux-thunk";

export type DemoAsyncAction = ThunkAction<void, AppState, null, DemoAction>;
export type DemoAsyncDispatch = ThunkDispatch<AppState, null, DemoAction>;
export type GetState = () => AppState;