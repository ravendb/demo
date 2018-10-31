import { combineReducers } from "redux";
import { errorReducer } from "./errorReducer";
import { demoReducer } from "./demoReducer";
import { AppState } from "../state";

export default combineReducers<AppState>({
    error: errorReducer,
    demos: demoReducer
});