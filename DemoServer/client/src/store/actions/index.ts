import { ThunkAction } from "redux-thunk";
import { AppState } from "../state";
import { ErrorAction } from "./errorActions";
import { DemoAction } from "./demoActions";
import { ProgressAction } from "./progressActions";

export type Action = ErrorAction | DemoAction | ProgressAction;
export type DemoThunkAction = ThunkAction<void, AppState, null, Action>;