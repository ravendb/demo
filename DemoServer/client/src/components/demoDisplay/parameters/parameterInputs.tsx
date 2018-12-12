import * as React from "react";
import { TextInputType, TextInputDataType } from "./parameterTypes";
import { FileInput, FileInputProps } from "../../helpers/FileInput";

export interface ParameterTextInputProps {
    inputType: TextInputType;
    datatype?: TextInputDataType;
    placeholder: string;
    onValueChange: (value: string) => void;
}

export function ParameterTextInput(props: ParameterTextInputProps) {
    const { inputType, datatype, placeholder, onValueChange } = props;

    return <input type={inputType} datatype={datatype}
        className="parameter-input"
        placeholder={placeholder}
        onChange={e => onValueChange(e.target.value)}
    />;
}

export function ParameterFileInput(props: FileInputProps) {
    return <FileInput className="parameter-input" {...props} />;
}