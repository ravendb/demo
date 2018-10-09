import * as actionNames from "./actionNames";

export type ErrorAction = ApiErrorAction;
export type CommonAction = ErrorAction;

export interface ApiErrorAction {
    type: actionNames.API_ERROR;
    error: any;
}

export function apiError(error: any) : ApiErrorAction {
    return {
        type: actionNames.API_ERROR,
        error
    };
}