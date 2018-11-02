import * as actionTypes from "./actionTypes";
import { DemoThunkAction, Action } from ".";
import { apiError } from "./errorActions";
import { DemoService, RunDemoService } from "../../utils/Services";
import { DemoDto } from "../../models/dtos";
import { ParameterPair, toDemoParamsDto } from "../../models/demoModels";
import { DemoThunkDispatch } from "../";

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

export interface RunDemoRequest {
    type: actionTypes.DEMO_RUN_REQUEST;
}

export interface RunDemoFailure {
    type: actionTypes.DEMO_RUN_FAILURE;
    error: any;
}

export interface RunDemoSuccess {
    type: actionTypes.DEMO_RUN_SUCCESS;
    results: object;
}

export interface InitDemoParams {
    type: actionTypes.DEMO_PARAMS_INIT;
    parameters: ParameterPair[];
}

export interface ChangeDemoParams {
    type: actionTypes.DEMO_PARAMS_CHANGE;
    name: string;
    value: any;
}

export type DemoAction = GetMetadataRequest | GetMetadataFailure | GetMetadataSuccess
    | RunDemoRequest | RunDemoFailure | RunDemoSuccess
    | InitDemoParams | ChangeDemoParams;

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

export function getMetadata(category: string, demo: string): DemoThunkAction {
    return async (dispatch: DemoThunkDispatch) => {
        dispatch(getMetadataRequest(category, demo));
        try {
            const result = await service.getMetadata(category, demo);
            dispatch(getMetadataSuccess(result));
        } catch (error) {
            dispatch(apiError(error));
            dispatch(getMetadataFailure(error))
        }
    }
}

function runDemoRequest(): RunDemoRequest {
    return {
        type: "DEMO_RUN_REQUEST"
    };
}

function runDemoFailure(error: any): RunDemoFailure {
    return {
        type: "DEMO_RUN_FAILURE",
        error
    };
}

function runDemoSuccess(results: object): RunDemoSuccess {
    return {
        type: "DEMO_RUN_SUCCESS",
        results
    };
}

export function runDemo(): DemoThunkAction {
    return async (dispatch: DemoThunkDispatch, getState) => {
        const { demos } = getState();
        const { demoSlug, parameters } = demos;
        dispatch(runDemoRequest());
        const demoService = new RunDemoService(demoSlug);
        try {
            const dto = toDemoParamsDto(parameters);
            const result = await demoService.run(dto);
            dispatch(runDemoSuccess(result));
        } catch (error) {
            dispatch(apiError(error));
            dispatch(runDemoFailure(error));
        }
    };
}

export function initDemoParams(parameters: ParameterPair[]): InitDemoParams {
    return {
        type: "DEMO_PARAMS_INIT",
        parameters
    };
}

export function changeDemoParams(name: string, value: any): ChangeDemoParams {
    return {
        type: "DEMO_PARAMS_CHANGE",
        name,
        value
    };
}