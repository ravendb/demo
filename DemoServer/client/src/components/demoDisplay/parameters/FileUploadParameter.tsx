import * as React from "react";
import { connect } from "react-redux";
import { ParameterFileInput } from "./parameterInputs";
import { ParameterLabel } from "./misc";
import { DemoThunkDispatch } from "../../../store";
import { changeDemoFileParam } from "../../../store/actions/parametersActions";
import { AppState } from "../../../store/state";

export interface FileUploadParameterOwnProps {
    name: string;
}

interface StateProps {
    validationError?: string;
}

interface DispatchProps {
    handleFileChange: (paramName: string, file: File) => void;
}

type Props = StateProps & DispatchProps & FileUploadParameterOwnProps;

function FileUploadParameterComponent(props: Props) {
    const { name, handleFileChange, validationError } = props;

    return <div className="parameter">
        <ParameterFileInput {...props} onFileChange={f => handleFileChange(name, f)} />
        <ParameterLabel {...props} />
        {!!validationError && <div className="validation-error">{validationError}</div>}
    </div>;
}

export const FileUploadParameter = connect<StateProps, DispatchProps, FileUploadParameterOwnProps>(
    ({ params }: AppState, ownProps: FileUploadParameterOwnProps): StateProps => {
        const { name } = ownProps;
        const error = params.fileParamsValidationErrors.find(x => x.paramName === name);

        return {
            validationError: error && error.error
        }
    },
    (dispatch: DemoThunkDispatch): DispatchProps => ({
        handleFileChange: (paramName: string, file: File) => dispatch(changeDemoFileParam(paramName, file))
    })
)(FileUploadParameterComponent);