import * as React from "react";
import { Parameters, ParametersProps } from "../Parameters";
import { Code } from "../Code";
import { NavPanel } from "./NavPanel";
import { Results, ResultDisplayProps } from "./Results";

interface DemoBodyOwnProps {
    results?: ResultDisplayProps;
    parameters?: ParametersProps;
}

interface DemoBodyStateProps {
}

type DemoBodyProps = DemoBodyOwnProps & DemoBodyStateProps;

export class DemoBodyDisplay extends React.Component<DemoBodyProps, {}> {
    render() {
        const { parameters, results } = this.props;
        const resultsId = "results";
        return <div className="demo-body">
            <div id="demo-body-container">
                {parameters && <Parameters {...parameters} />}
                <Code />
                <NavPanel
                    onWalkthroughClick={() => alert("WALKTHROUGH clicked")}
                    resultsElementId={resultsId}
                />
                {results && <Results elementId={resultsId} {...results} />}
            </div>
        </div>;
    }
}