import { combineReducers } from "redux";
import { exampleReducer } from "../features/example/reducers";
import { AppState } from "./state";
import { errorReducer } from "../features/common/reducers";

export default combineReducers<AppState>({
    example: exampleReducer,
    error: errorReducer
});