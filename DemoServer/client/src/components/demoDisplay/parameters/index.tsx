import * as React from "react";
import { connect } from "react-redux";
import { ParameterPair } from "../../../models/demoModels";
import { DemoThunkDispatch } from "../../../store";
import { initDemoParams } from "../../../store/actions/demoActions";
import { TextParameterOwnProps, TextParameter } from "./TextParameter";
import { FileUploadParameterOwnProps, FileUploadParameter } from "./FileUploadParameter";

type TextParameterItem = TextParameterOwnProps & {
    paramKind: "text-param";
}

type FileUploadParameterItem = FileUploadParameterOwnProps & {
    paramKind: "file-upload-param";
}

export type ParamDefinition = TextParameterItem | FileUploadParameterItem;

export interface ParameterOwnProps {
    paramDefinitions?: ParamDefinition[];
}

interface DispatchProps {
    initParams: (parameters: ParameterPair[]) => void;
}

type ParametersProps = ParameterOwnProps & DispatchProps;

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

    renderParameter(paramDefinition: ParamDefinition, index: number) {
        const { name } = paramDefinition;
        const key = `parameter_${name}_${index}`;

        switch (paramDefinition.paramKind) {
            case "text-param":
                return <TextParameter {...paramDefinition} key={key} />;
            
            case "file-upload-param":
                return <FileUploadParameter {...paramDefinition} key={key} />;
        }

        return null;
    }

    render() {
        const { paramDefinitions } = this.props;
        if (!paramDefinitions) {
            return null;
        }

        return <div className="parameters">
            {paramDefinitions.map((x, i) => this.renderParameter(x, i))}
        </div>;
    }
}

export const Parameters = connect<{}, DispatchProps, ParameterOwnProps>(
    () => ({}),
    (dispatch: DemoThunkDispatch): DispatchProps => ({
        initParams: (parameters: ParameterPair[]) => dispatch(initDemoParams(parameters))
    })
)(ParametersDisplay);