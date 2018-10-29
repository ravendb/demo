import * as React from "react";
import { Parameters, ParametersProps } from "../Parameters";
import { Code, CodeProps } from "../Code";
import { NavPanel } from "./NavPanel";
import { Results, ResultDisplayProps } from "./Results";

interface DemoBodyProps {
    code: CodeProps;
    results?: ResultDisplayProps;
    parameters?: ParametersProps;
}

export class DemoBody extends React.Component<DemoBodyProps, {}> {
    render() {
        const { code, parameters, results } = this.props;
        const resultsId = "results";
        return <div className="demo-body">
            <div id="demo-body-container">
                {parameters && <Parameters {...parameters} />}
                <Code {...code} />
                <NavPanel
                    onWalkthroughClick={() => alert("WALKTHROUGH clicked")}
                    onRunScriptClicked={() => console.log("RUN SCRIPT clicked")}
                    resultsElementId={resultsId}
                />
                {results && <Results elementId={resultsId} {...results} />}
            </div>
        </div>;
    }
}