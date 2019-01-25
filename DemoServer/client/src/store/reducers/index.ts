import { combineReducers } from "redux";
import { connectRouter } from "connected-react-router";
import { History } from "history";
import { errorReducer } from "./error";
import { demoReducer } from "./demo";
import { AppState } from "../state";
import { parametersReducer } from "./parameters";
import { settingsReducer } from "./settings";

export default (history: History) => combineReducers<AppState>({
    router: connectRouter(history),
    error: errorReducer,
    demos: demoReducer,
    params: parametersReducer,
    settings: settingsReducer
});
