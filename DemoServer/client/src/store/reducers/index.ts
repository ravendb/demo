import { combineReducers } from "redux";
import { History } from "history";
import { errorReducer } from "./errorReducer";
import { demoReducer } from "./demoReducer";
import { AppState } from "../state";
import { parametersReducer } from "./parametersReducer";
import { connectRouter } from "connected-react-router";

export default (history: History) => combineReducers<AppState>({
    router: connectRouter(history),
    error: errorReducer,
    demos: demoReducer,
    params: parametersReducer
});
