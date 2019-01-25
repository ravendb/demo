import * as actionTypes from "./actionTypes";

export interface ApiError {
    type: actionTypes.API_ERROR;
    error: any;
}

export interface ReloadOnApiError {
    type: actionTypes.API_ERROR_RELOAD;
}

export type ErrorAction = ApiError | ReloadOnApiError;

export function apiError(error: any): ApiError {
    return {
        type: "API_ERROR",
        error
    };
}

export function reloadOnApiError(): ReloadOnApiError {
    return {
        type: "API_ERROR_RELOAD"
    };
}
