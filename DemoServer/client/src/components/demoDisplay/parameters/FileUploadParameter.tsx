import * as React from "react";
import { connect } from "react-redux";
import { ParameterFileInput } from "./parameterInputs";
import { ParameterLabel } from "./ParameterLabel";
import { DemoThunkDispatch } from "../../../store";
import { changeDemoFileParam } from "../../../store/actions/demoActions";

export interface FileUploadParameterOwnProps {
    name: string;
}

interface DispatchProps {
    handleFileChange: (paramName: string, file: File) => void;
}

type Props = DispatchProps & FileUploadParameterOwnProps;

function FileUploadParameterComponent(props: Props) {
    const { name, handleFileChange } = props;

    return <div className="parameter">
        <ParameterFileInput {...props} onFileChange={f => handleFileChange(name, f)} />
        <ParameterLabel {...props} />
    </div>;
}

export const FileUploadParameter = connect<{}, DispatchProps, FileUploadParameterOwnProps>(
    () => ({}),
    (dispatch: DemoThunkDispatch): DispatchProps => ({
        handleFileChange: (paramName: string, file: File) => dispatch(changeDemoFileParam(paramName, file))
    })
)(FileUploadParameterComponent);