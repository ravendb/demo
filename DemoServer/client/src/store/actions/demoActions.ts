import * as actionTypes from "./actionTypes";
import { DemoThunkAction, Action } from ".";
import { apiError } from "./errorActions";
import { DemoService, RunDemoService } from "../../utils/Services";
import { DemoDto } from "../../models/dtos";
import { ParameterPair, toDemoParamsDto, UserProgressDto } from "../../models/demoModels";
import { DemoThunkDispatch } from "../";

const service = new DemoService();

export interface GetProgressRequest {
    type: actionTypes.DEMO_GET_PROGRESS_REQUEST;
}

export interface GetProgressFailure {
    type: actionTypes.DEMO_GET_PROGRESS_FAILURE;
    error: any;
}

export interface GetProgressSuccess {
    type: actionTypes.DEMO_GET_PROGRESS_SUCCESS;
    result: UserProgressDto;
}

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

export interface SetPrerequisitesRequest {
    type: actionTypes.DEMO_SET_PREREQUISITES_REQUEST;
}

export interface SetPrerequisitesFailure {
    type: actionTypes.DEMO_SET_PREREQUISITES_FAILURE;
    error: any;
}

export interface SetPrerequisitesSuccess {
    type: actionTypes.DEMO_SET_PREREQUISITES_SUCCESS;
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

export interface HideResults {
    type: actionTypes.DEMO_HIDE_RESULTS;
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

export type DemoAction = GetProgressRequest | GetProgressFailure | GetProgressSuccess
    | GetMetadataRequest | GetMetadataFailure | GetMetadataSuccess
    | SetPrerequisitesRequest | SetPrerequisitesFailure | SetPrerequisitesSuccess
    | RunDemoRequest | RunDemoFailure | RunDemoSuccess
    | HideResults
    | InitDemoParams | ChangeDemoParams;

function getProgressRequest(): GetProgressRequest {
    return {
        type: "DEMO_GET_PROGRESS_REQUEST"
    };
}

function getProgressFailure(error: any): GetProgressFailure {
    return {
        type: "DEMO_GET_PROGRESS_FAILURE",
        error
    };
}

function getProgressSuccess(result: UserProgressDto): GetProgressSuccess {
    return {
        type: "DEMO_GET_PROGRESS_SUCCESS",
        result
    };
}

export function getProgress(): DemoThunkAction {
    return async (dispatch: DemoThunkDispatch) => {
        dispatch(getProgressRequest());
        try {
            const result = await service.getProgress();
            dispatch(getProgressSuccess(result));
        } catch (error) {
            dispatch(apiError(error));
            dispatch(getProgressFailure(error));
        }
    }
}

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
            dispatch(setPrerequisites());
        } catch (error) {
            dispatch(apiError(error));
            dispatch(getMetadataFailure(error))
        }
    }
}

const createDemoService = (demoSlug: string) => new RunDemoService(demoSlug);

function setPrerequisitesRequest(): SetPrerequisitesRequest {
    return {
        type: "DEMO_SET_PREREQUISITES_REQUEST"
    };
}

function setPrerequisitesFailure(error: any): SetPrerequisitesFailure {
    return {
        type: "DEMO_SET_PREREQUISITES_FAILURE",
        error
    };
}

function setPrerequisitesSuccess(): SetPrerequisitesSuccess {
    return {
        type: "DEMO_SET_PREREQUISITES_SUCCESS"
    };
}

function setPrerequisites(): DemoThunkAction {
    return async (dispatch: DemoThunkDispatch, getState) => {
        const { demos } = getState();
        const { demoSlug } = demos;
        dispatch(setPrerequisitesRequest());
        const demoService = createDemoService(demoSlug);
        try {
            await demoService.setPrerequisites();
            dispatch(setPrerequisitesSuccess());
        } catch (error) {
            dispatch(apiError(error));
            dispatch(setPrerequisitesFailure(error));
        }
    };
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
        const demoService = createDemoService(demoSlug);
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

export function hideResults(): HideResults {
    return {
        type: "DEMO_HIDE_RESULTS"
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