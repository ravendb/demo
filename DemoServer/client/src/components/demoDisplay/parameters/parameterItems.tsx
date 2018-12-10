import * as React from "react";
import { ParameterTextInput, ParameterTextInputProps, ParameterFileInput, ParameterFileInputProps } from "./parameterInputs";
import { ParameterLabel, ParameterLabelProps } from "./ParameterLabel";

export type TextParameterProps = ParameterLabelProps & ParameterTextInputProps;

export function TextParameter(props: TextParameterProps) {
    return <div className="parameter">
        <ParameterTextInput {...props} />
        <ParameterLabel {...props} />
    </div>;
}

export type FileUploadParameterProps = ParameterFileInputProps & {
    name: string;
};

export function FileUploadParameter(props: FileUploadParameterProps) {
    return <div className="parameter">
        <ParameterFileInput {...props} />
        <ParameterLabel {...props} />
    </div>;
}