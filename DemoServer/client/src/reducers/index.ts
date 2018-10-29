import { combineReducers } from "redux";
import { AppState } from "../store/state";
import { exampleReducer } from "./exampleReducer";
import { errorReducer } from "./errorReducer";
import { demoReducer } from "./demoReducer";

export default combineReducers<AppState>({
    example: exampleReducer,
    error: errorReducer,
    demo: demoReducer
});