import * as React from "react";
import { ParameterPair } from "../../models/demoModels";
import { DemoThunkDispatch } from "../../store";
import { AppState } from "../../store/state";
import { connect } from "react-redux";
import { changeDemoParams, initDemoParams } from "../../store/actions/demoActions";

export interface ParameterItem {
    type: "text" | "date" | "number";
    name: string;
    placeholder: string;
    datatype?: "integer" | "float"
}

export interface ParameterOwnProps {
    paramDefinitions: ParameterItem[];
}

interface ParameterStateProps {
}

interface ParameterDispatchProps {
    initParams: (parameters: ParameterPair[]) => void;
    handleValueChange: (paramName: string, value: any) => void;
}

type ParametersProps = ParameterOwnProps & ParameterStateProps & ParameterDispatchProps;

function toParameterPair({ name, placeholder }: ParameterItem): ParameterPair {
    return {
        name,
        value: placeholder
    };
}

class ParametersDisplay extends React.Component<ParametersProps, {}> {
    componentDidMount() {
        const { initParams, paramDefinitions } = this.props;
        const paramPairs = paramDefinitions.map(toParameterPair);
        initParams(paramPairs);
    }

    displayItem(item: ParameterItem, index: number) {
        const { type, name, placeholder, datatype } = item;
        const { handleValueChange } = this.props;

        return <div className="parameter" key={`parameter_${name}_${index}`}>
            <input type={type} datatype={datatype}
                className="parameter-input"
                placeholder={placeholder}
                onChange={e => handleValueChange(name, e.target.value)} />
            <div className="parameter-label">
                <div className="name">{name}</div>
                <div className="type"></div>
            </div>
        </div>;
    }

    render() {
        const { paramDefinitions } = this.props;
        return <div className="parameters">
            {paramDefinitions.map((x, i) => this.displayItem(x, i))}
        </div>;
    }
}

function mapStateToProps({ demos }: AppState): ParameterStateProps {
    return {
        paramValues: demos.parameters
    };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): ParameterDispatchProps {
    return {
        initParams: (parameters: ParameterPair[]) => dispatch(initDemoParams(parameters)),
        handleValueChange: (paramName: string, value: any) => dispatch(changeDemoParams(paramName, value))
    };
}

export const Parameters = connect<ParameterStateProps, ParameterDispatchProps, ParameterOwnProps>(mapStateToProps, mapDispatchToProps)(ParametersDisplay);