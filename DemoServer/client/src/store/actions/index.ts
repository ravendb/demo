import { ThunkAction } from "redux-thunk";
import { AppState } from "../state";
import { ErrorAction } from "./errorActions";
import { DemoAction } from "./demoActions";
import { ParametersAction } from "./parametersActions";
import { CallHistoryMethodAction } from "connected-react-router";

export type Action = ErrorAction | DemoAction | ParametersAction | CallHistoryMethodAction;
export type DemoThunkAction = ThunkAction<void, AppState, null, Action>;
