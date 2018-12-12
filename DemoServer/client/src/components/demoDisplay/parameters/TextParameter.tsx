import * as React from "react";
import { connect } from "react-redux";
import { ParameterTextInput } from "./parameterInputs";
import { ParameterLabel } from "./ParameterLabel";
import { DemoThunkDispatch } from "../../../store";
import { changeDemoParams } from "../../../store/actions/demoActions";
import { TextInputType, TextInputDataType } from "./parameterTypes";

export interface TextParameterOwnProps {
    inputType: TextInputType;
    datatype?: TextInputDataType;
    placeholder: string;
    name: string;
};

interface DispatchProps {
    handleValueChange: (paramName: string, value: any) => void;
}

type Props = TextParameterOwnProps & DispatchProps;

function TextParameterComponent(props: Props) {
    const { name, handleValueChange } = props;

    return <div className="parameter">
        <ParameterTextInput {...props} onValueChange={v => handleValueChange(name, v)} />
        <ParameterLabel {...props} />
    </div>;
}

export const TextParameter = connect<{}, DispatchProps, TextParameterOwnProps>(
    () => ({}),
    (dispatch: DemoThunkDispatch): DispatchProps => ({
        handleValueChange: (paramName: string, value: any) => dispatch(changeDemoParams(paramName, value)),
    })
)(TextParameterComponent);