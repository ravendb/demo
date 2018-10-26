import { combineReducers } from "redux";
import { AppState } from "../store/state";
import { exampleReducer } from "./exampleReducer";
import { errorReducer } from "./errorReducer";

export default combineReducers<AppState>({
    example: exampleReducer,
    error: errorReducer
});