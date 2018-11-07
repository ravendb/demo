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
    | SetPrerequisitesRequest | SetPrerequisitesFailure | SetPrerequisitesSuccess
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