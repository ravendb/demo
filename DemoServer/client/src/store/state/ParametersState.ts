import { ParameterPair } from "../../models/demoModels";

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