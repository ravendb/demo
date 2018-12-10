import * as React from "react";
import { TextParameterType } from "./parameterTypes";
import { FileInput } from "../../helpers/FileInput";

export interface ParameterTextInputProps {
    type: TextParameterType;
    datatype?: "integer" | "float";
    placeholder: string;
    onValueChange: (value: string) => void;
}

export function ParameterTextInput(props: ParameterTextInputProps) {
    const { type, datatype, placeholder, onValueChange } = props;

    return <input type={type} datatype={datatype}
        className="parameter-input"
        placeholder={placeholder}
        onChange={e => onValueChange(e.target.value)}
    />;
}

export interface ParameterFileInputProps {
    onFileChange: (file: File) => void;
}

export function ParameterFileInput(props: ParameterFileInputProps) {
    return <FileInput className="parameter-input" {...props} />;
}