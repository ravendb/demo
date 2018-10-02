import * as actionNames from "./actionNames";
import { ExampleDataDto } from "./models";
import { ExampleService } from "../../utils/Services";
import { DemoAsyncDispatch, DemoAsyncAction } from "../../store/async";
import { apiError } from "../common/actions";

const service = new ExampleService();

export interface FetchResult {
    type: actionNames.FETCH_RESULT_REQUEST;
}

export interface FetchResultFailure {
    type: actionNames.FETCH_RESULT_FAILURE;
    error: any;
}

export interface FetchResultSuccess {
    type: actionNames.FETCH_RESULT_SUCCESS;
    result: ExampleDataDto;
}

export type ExampleAction = FetchResult | FetchResultFailure | FetchResultSuccess;

function requestResult(): FetchResult {
    return {
        type: actionNames.FETCH_RESULT_REQUEST
    };
}

function fetchResultFailure(error: any): FetchResultFailure {
    return {
        type: actionNames.FETCH_RESULT_FAILURE,
        error
    };
}

function fetchResultSuccess(result: ExampleDataDto): FetchResultSuccess {
    return {
        type: actionNames.FETCH_RESULT_SUCCESS,
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