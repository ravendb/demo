import * as actionTypes from "./actionTypes";
import { DemoService } from "../utils/Services";
import { DemoDto } from "../models/dtos";
import { DemoAsyncAction } from ".";
import { DemoAsyncDispatch } from "../store/async";
import { apiError } from "./errorActions";

const service = new DemoService();

export interface GetMetadataRequest {
    type: actionTypes.DEMO_GET_METADATA_REQUEST;
    category: string;
    demo: string;
}

export interface GetMetadataFailure {
    type: actionTypes.DEMO_GET_METADATA_FAILURE;
    error: any;
}

export interface GetMetadataSuccess {
    type: actionTypes.DEMO_GET_METADATA_SUCCESS;
    result: DemoDto;
}

export type DemoAction = GetMetadataRequest | GetMetadataFailure | GetMetadataSuccess;

function getMetadataRequest(category: string, demo: string): GetMetadataRequest {
    return {
        type: "DEMO_GET_METADATA_REQUEST",
        category,
        demo
    };
}

function getMetadataFailure(error: any): GetMetadataFailure {
    return {
        type: "DEMO_GET_METADATA_FAILURE",
        error
    };
}

function getMetadataSuccess(result: DemoDto): GetMetadataSuccess {
    return {
        type: "DEMO_GET_METADATA_SUCCESS",
        result
    };
}

export function getMetadata(category: string, demo: string): DemoAsyncAction {
    return (dispatch: DemoAsyncDispatch) => {
        dispatch(getMetadataRequest(category, demo));

        return service.getMetadata(category, demo)
            .then(result => {
                dispatch(getMetadataSuccess(result));
            })
            .catch(error => {
                dispatch(apiError(error));
                dispatch(getMetadataFailure(error));
            });
    }
}