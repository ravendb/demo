import * as actionTypes from "./actionTypes";

export type ErrorAction = ApiErrorAction;

export interface ApiErrorAction {
    type: actionTypes.API_ERROR;
    error: any;
}

export function apiError(error: any) : ApiErrorAction {
    return {
        type: "API_ERROR",
        error
    };
}