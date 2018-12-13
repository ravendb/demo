import { combineReducers } from "redux";
import { errorReducer } from "./errorReducer";
import { demoReducer } from "./demoReducer";
import { AppState } from "../state";
import { progressReducer } from "./progressReducer";
import { parametersReducer } from "./parametersReducer";

export default combineReducers<AppState>({
    error: errorReducer,
    demos: demoReducer,
    params: parametersReducer,
    progress: progressReducer
});