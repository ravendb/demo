import * as actionTypes from "./actionTypes";
import { ParameterPair } from "../../models/demoModels";

export interface InitDemoParams {
    type: actionTypes.PARAMS_INIT;
    parameters: ParameterPair[];
}

export interface ChangeDemoParams {
    type: actionTypes.PARAMS_CHANGE;
    name: string;
    value: any;
}

export interface ChangeDemoFileParam {
    type: actionTypes.PARAMS_CHANGE_FILE;
    name: string;
    file: File;
}

export interface HideInvalidUploadMessage {
    type: actionTypes.PARAMS_HIDE_INVALID_UPLOAD_MESSAGE;
}

export type ParametersAction = InitDemoParams | ChangeDemoParams | ChangeDemoFileParam
    | HideInvalidUploadMessage;


export function initDemoParams(parameters: ParameterPair[]): InitDemoParams {
    return {
        type: "PARAMS_INIT",
        parameters
    };
}

export function changeDemoParams(name: string, value: any): ChangeDemoParams {
    return {
        type: "PARAMS_CHANGE",
        name,
        value
    };
}

export function changeDemoFileParam(name: string, file: File): ChangeDemoFileParam {
    return {
        type: "PARAMS_CHANGE_FILE",
        name,
        file
    };
}

export function hideInvalidUploadMessage(): HideInvalidUploadMessage {
    return {
        type: "PARAMS_HIDE_INVALID_UPLOAD_MESSAGE"
    };
}