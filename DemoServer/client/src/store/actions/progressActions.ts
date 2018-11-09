import * as actionTypes from "./actionTypes";
import { DemoVersionDto } from "../../models/dtos";
import { DemoThunkAction } from ".";
import { DemoThunkDispatch } from "..";
import { DemoService } from "../../utils/Services";
import { apiError } from "./errorActions";

const service = new DemoService();

export interface GetVersionsRequest {
    type: actionTypes.PROGRESS_GET_VERSIONS_REQUEST;
}

export interface GetVersionsFailure {
    type: actionTypes.PROGRESS_GET_VERSIONS_FAILURE;
    error: any;
}

export interface GetVersionsSuccess {
    type: actionTypes.PROGRESS_GET_VERSIONS_SUCCESS;
    results: DemoVersionDto[];
}

export interface SaveProgress {
    type: actionTypes.PROGRESS_SAVE;
    category: string;
    demo: string;
}

export type ProgressAction = GetVersionsRequest | GetVersionsFailure | GetVersionsSuccess
    | SaveProgress;

function getVersionsRequest(): GetVersionsRequest {
    return {
        type: "PROGRESS_GET_VERSIONS_REQUEST"
    };
}

function getVersionsFailure(error: any): GetVersionsFailure {
    return {
        type: "PROGRESS_GET_VERSIONS_FAILURE",
        error
    };
}

function getVersionsSuccess(results: DemoVersionDto[]): GetVersionsSuccess {
    return {
        type: "PROGRESS_GET_VERSIONS_SUCCESS",
        results
    };
}

export function getVersions(): DemoThunkAction {
    return async (dispatch: DemoThunkDispatch) => {
        dispatch(getVersionsRequest());
        try {
            const results = await service.getVersions();
            dispatch(getVersionsSuccess(results));
        } catch (error) {
            dispatch(apiError(error));
            dispatch(getVersionsFailure(error));
        }
    }
}

export function updateProgress(category: string, demo: string): SaveProgress {
    return {
        type: "PROGRESS_SAVE",
        category,
        demo
    };
}