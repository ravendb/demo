import { ThunkAction } from "redux-thunk";
import { AppState } from "../state";
import { ErrorAction } from "./error";
import { DemoAction } from "./demo";
import { ParametersAction } from "./parameters";
import { CallHistoryMethodAction } from "connected-react-router";
import { SettingsAction } from "./settings";

export type Action = ErrorAction | DemoAction | ParametersAction | SettingsAction | CallHistoryMethodAction;
export type DemoThunkAction = ThunkAction<void, AppState, null, Action>;
