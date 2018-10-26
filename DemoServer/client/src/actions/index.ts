import { ExampleAction } from "./exampleActions";
import { DemoAction } from ".";
import { ThunkAction } from "redux-thunk";
import { AppState } from "../store/state";
import { ErrorAction } from "./errorActions";

export type DemoAction = ExampleAction | ErrorAction;
export type DemoAsyncAction = ThunkAction<void, AppState, null, DemoAction>;