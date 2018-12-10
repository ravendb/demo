import * as actionTypes from "./actionTypes";
import { DemoThunkAction } from ".";
import { apiError } from "./errorActions";
import { DemoService, RunDemoService } from "../../utils/Services";
import { DemoDto } from "../../models/dtos";
import { ParameterPair, toDemoParamsDto } from "../../models/demoModels";
import { DemoThunkDispatch } from "../";
import { updateProgress } from "./progressActions";
import clipboardCopy = require("clipboard-copy");
import { FilesCache } from "../../utils/FilesCache";
import { FormFile } from "../../utils/ApiClient";

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

export interface ChangeDemoFileParam {
    type: actionTypes.DEMO_PARAMS_CHANGE_FILE;
    name: string;
    file: File;
}

export interface ToggleDemoShareMessage {
    type: actionTypes.DEMO_TOGGLE_SHARE_MESSAGE,
    show: boolean;
}

export type DemoAction = GetMetadataRequest | GetMetadataFailure | GetMetadataSuccess
    | SetPrerequisitesRequest | SetPrerequisitesFailure | SetPrerequisitesSuccess
    | RunDemoRequest | RunDemoFailure | RunDemoSuccess
    | HideResults
    | InitDemoParams | ChangeDemoParams | ChangeDemoFileParam
    | ToggleDemoShareMessage;

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
            
            if (result.nonInteractive) {
                dispatch(setPrerequisitesSuccess());
            } else {
                dispatch(setPrerequisites());
            }
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
        const { demoSlug, demo } = demos;

        if (demo.nonInteractive) {
            return;
        }

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
        const { categorySlug, demoSlug, parameters, demo, attachmentNamesToUpload } = demos;

        if (demo.nonInteractive) {
            return;
        }

        const demoService = createDemoService(demoSlug);

        dispatch(runDemoRequest());
        try {
            const dto = toDemoParamsDto(parameters);
            let result: object = null;

            if (attachmentNamesToUpload.length > 0) {
                const fileEntries = FilesCache.getForKeys(attachmentNamesToUpload);

                const toUpload: FormFile[] = fileEntries.map(x => ({
                    name: x.key,
                    file: x.value
                }));

                result = await demoService.runWithFiles(dto, toUpload);
            } else {
                result = await demoService.run(dto);
            }

            dispatch(runDemoSuccess(result));
            dispatch(updateProgress(categorySlug, demoSlug));
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

export function changeDemoFileParam(name: string, file: File): ChangeDemoFileParam {
    return {
        type: "DEMO_PARAMS_CHANGE_FILE",
        name,
        file
    }
}

export function toggleDemoShareMessage(show: boolean): ToggleDemoShareMessage {
    return {
        type: "DEMO_TOGGLE_SHARE_MESSAGE",
        show
    }
}

export function shareDemo(): DemoThunkAction {
    return async (dispatch: DemoThunkDispatch) => {
        clipboardCopy(window.location.href);
        dispatch(toggleDemoShareMessage(true));
    }
}