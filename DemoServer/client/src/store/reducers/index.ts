import { combineReducers } from "redux";
import { errorReducer } from "./errorReducer";
import { demoReducer } from "./demoReducer";
import { AppState } from "../state";
import { progressReducer } from "./progressReducer";

export default combineReducers<AppState>({
    error: errorReducer,
    demos: demoReducer,
    progress: progressReducer
});