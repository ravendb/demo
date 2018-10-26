import * as actionTypes from "./actionTypes";
import { ExampleService } from "../utils/Services";
import { ExampleDataDto } from "../models/exampleModels";
import { DemoAsyncDispatch } from "../store/async";
import { DemoAsyncAction } from ".";
import { apiError } from "./errorActions";

const service = new ExampleService();

export interface FetchResult {
    type: actionTypes.FETCH_RESULT_REQUEST;
}

export interface FetchResultFailure {
    type: actionTypes.FETCH_RESULT_FAILURE;
    error: any;
}

export interface FetchResultSuccess {
    type: actionTypes.FETCH_RESULT_SUCCESS;
    result: ExampleDataDto;
}

export type ExampleAction = FetchResult | FetchResultFailure | FetchResultSuccess;

function requestResult(): FetchResult {
    return {
        type: "FETCH_RESULT_REQUEST"
    };
}

function fetchResultFailure(error: any): FetchResultFailure {
    return {
        type: "FETCH_RESULT_FAILURE",
        error
    };
}

function fetchResultSuccess(result: ExampleDataDto): FetchResultSuccess {
    return {
        type: "FETCH_RESULT_SUCCESS",
        result
    };
}

export function fetchResult(): DemoAsyncAction {
    return (dispatch: DemoAsyncDispatch) => {
        dispatch(requestResult());

        return service.getData()
            .then(result => {
                dispatch(fetchResultSuccess(result));
            })
            .catch(error => {
                dispatch(apiError(error));
                dispatch(fetchResultFailure(error));
            });
    }
}