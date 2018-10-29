import { ExampleAction } from "./exampleActions";
import { ThunkAction } from "redux-thunk";
import { AppState } from "../store/state";
import { ErrorAction } from "./errorActions";
import { DemoAction } from "./demoActions";

export type Action = ExampleAction | ErrorAction | DemoAction;
export type DemoAsyncAction = ThunkAction<void, AppState, null, Action>;