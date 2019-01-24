import { ParameterPair } from "../../models/demo";

interface FileParamValidationError {
    paramName: string;
    error: string;
}

export interface ParametersState {
    parameters: ParameterPair[];
    attachmentNamesToUpload: string[];
    showInvalidUploadMessage: boolean;
    fileParamsValidationErrors: FileParamValidationError[];
}