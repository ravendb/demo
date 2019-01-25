import { modifyState } from "../state";
import { ParametersState } from "../state/parameters";
import { ParametersAction } from "../actions/parameters";
import { FilesCache } from "../../utils/FilesCache";
import { isInvalidFileSize, fileSizeLimitMB } from "../../utils/fileUtils";

const initialState: ParametersState = {
    parameters: [],
    attachmentNamesToUpload: [],
    showInvalidUploadMessage: false,
    fileParamsValidationErrors: []
};

export function parametersReducer(state: ParametersState = initialState, action: ParametersAction): ParametersState {
    switch (action.type) {
        case "PARAMS_INIT":
            FilesCache.clear();
            return modifyState(state, s => {
                s.parameters = action.parameters;
                s.attachmentNamesToUpload = [];
                s.fileParamsValidationErrors = [];
            });

        case "PARAMS_CHANGE":
            return modifyState(state, s => {
                s.parameters = s.parameters.map(x => x.name === action.name
                    ? { ...x, value: action.value }
                    : x);
            });

        case "PARAMS_CHANGE_FILE":
            return modifyState(state, s => {
                const { name, file } = action;

                const isInvalid = isInvalidFileSize(file);
                const isMarkedAsInvalid = !!(s.fileParamsValidationErrors.find(x => x.paramName === name));

                if (isInvalid && !isMarkedAsInvalid) {
                    s.fileParamsValidationErrors.push({
                        paramName: name,
                        error: `File is too large (more than ${fileSizeLimitMB} MB).`
                    });
                }

                if (!isInvalid && isMarkedAsInvalid) {
                    s.fileParamsValidationErrors = s.fileParamsValidationErrors.filter(x => x.paramName !== name);
                }

                if (isInvalid) {
                    FilesCache.remove(name);
                    s.showInvalidUploadMessage = true;
                    s.attachmentNamesToUpload = s.attachmentNamesToUpload.filter(x => x !== name);
                    return;
                }

                FilesCache.addOrUpdate(name, file);

                const alreadyStored = s.attachmentNamesToUpload.find(x => x === name);
                if (!alreadyStored) {
                    s.attachmentNamesToUpload.push(name);
                }
            });

        case "PARAMS_HIDE_INVALID_UPLOAD_MESSAGE":
            return modifyState(state, s => {
                s.showInvalidUploadMessage = false;
            });
    }

    return state;
}
