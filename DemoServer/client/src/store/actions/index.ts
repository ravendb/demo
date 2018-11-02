import { ThunkAction } from "redux-thunk";
import { AppState } from "../state";
import { ErrorAction } from "./errorActions";
import { DemoAction } from "./demoActions";

export type Action = ErrorAction | DemoAction;
export type DemoThunkAction = ThunkAction<void, AppState, null, Action>;