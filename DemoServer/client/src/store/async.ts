import { DemoAction } from "../actions";
import { AppState } from "./state";
import { ThunkDispatch } from "redux-thunk";

export type DemoAsyncDispatch = ThunkDispatch<AppState, null, DemoAction>;
export type GetState = () => AppState;