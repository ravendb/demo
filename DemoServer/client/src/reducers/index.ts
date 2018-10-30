import { combineReducers } from "redux";
import { AppState } from "../store/state";
import { exampleReducer } from "./exampleReducer";
import { errorReducer } from "./errorReducer";
import { demoReducer } from "./demoReducer";
import { routerReducer } from "react-router-redux";

export default combineReducers<AppState>({
    example: exampleReducer,
    error: errorReducer,
    demos: demoReducer,
    routing: routerReducer
});