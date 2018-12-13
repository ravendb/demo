import { ThunkAction } from "redux-thunk";
import { AppState } from "../state";
import { ErrorAction } from "./errorActions";
import { DemoAction } from "./demoActions";
import { ProgressAction } from "./progressActions";
import { ParametersAction } from "./parametersActions";

export type Action = ErrorAction | DemoAction | ParametersAction | ProgressAction;
export type DemoThunkAction = ThunkAction<void, AppState, null, Action>;