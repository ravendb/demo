import * as React from "react";
import { ParameterPair } from "../../../models/demoModels";
import { DemoThunkDispatch } from "../../../store";
import { connect } from "react-redux";
import { changeDemoParams, initDemoParams, changeDemoFileParam } from "../../../store/actions/demoActions";
import { TextParameter, FileUploadParameter } from "./parameterItems";
import { TextParameterType } from "./parameterTypes";

export type TextParameterItem = {
    name: string;
    type: TextParameterType;
    datatype?: "integer" | "float";
    placeholder: string;
}

export interface FileUploadParameterItem {
    name: string;
}

export interface ParameterOwnProps {
    paramDefinitions?: TextParameterItem[];
    fileUploadDefinitions?: FileUploadParameterItem[];
}

interface ParameterDispatchProps {
    initParams: (parameters: ParameterPair[]) => void;
    handleValueChange: (paramName: string, value: any) => void;
    handleFileChange: (paramName: string, file: File) => void;
}

type ParametersProps = ParameterOwnProps & ParameterDispatchProps;

function toParameterPair({ name, placeholder }: TextParameterItem): ParameterPair {
    return {
        name,
        value: placeholder
    };
}

class ParametersDisplay extends React.Component<ParametersProps, {}> {
    componentDidMount() {
        const { initParams, paramDefinitions } = this.props;
        if (paramDefinitions) {
            const paramPairs = paramDefinitions.map(toParameterPair);
            initParams(paramPairs);
        }
    }

    textParameter(item: TextParameterItem, index: number) {
        const { name } = item;
        const { handleValueChange } = this.props;

        return <TextParameter {...item}
            onValueChange={v => handleValueChange(name, v)}
            key={`parameter_${name}_${index}`}
        />;
    }

    fileUploadParameter(item: FileUploadParameterItem, index: number) {
        const { name } = item;
        const { handleFileChange } = this.props;

        return <FileUploadParameter {...item}
            onFileChange={f => handleFileChange(name, f)}
            key={`parameter_${name}_${index}`}
        />;
    }

    render() {
        const { paramDefinitions, fileUploadDefinitions } = this.props;
        if (!paramDefinitions && !fileUploadDefinitions) {
            return null;
        }

        return <div className="parameters">
            {paramDefinitions && paramDefinitions.map((x, i) => this.textParameter(x, i))}
            {fileUploadDefinitions && fileUploadDefinitions.map((x, i) => this.fileUploadParameter(x, i))}
        </div>;
    }
}

function mapStateToProps() {
    return {
    };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): ParameterDispatchProps {
    return {
        initParams: (parameters: ParameterPair[]) => dispatch(initDemoParams(parameters)),
        handleValueChange: (paramName: string, value: any) => dispatch(changeDemoParams(paramName, value)),
        handleFileChange: (paramName: string, file: File) => dispatch(changeDemoFileParam(paramName, file))
    };
}

export const Parameters = connect<{}, ParameterDispatchProps, ParameterOwnProps>(mapStateToProps, mapDispatchToProps)(ParametersDisplay);