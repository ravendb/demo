import * as React from "react";
import { Parameters, ParameterOwnProps } from "../Parameters";
import { Code } from "../Code";
import { NavPanel } from "./NavPanel";
import { ResultsPanel } from "../results/ResultsPanel";

export type DemoBodyProps = ParameterOwnProps & {
    resultsComponents: () => JSX.Element;
}

export class DemoBody extends React.Component<DemoBodyProps, {}> {
    render() {
        const { paramDefinitions, resultsComponents } = this.props;
        const resultsId = "results";
        return <div className="demo-body">
            <div id="demo-body-container">
                {paramDefinitions && <Parameters paramDefinitions={paramDefinitions} />}
                <Code />
                <NavPanel
                    onWalkthroughClick={() => alert("WALKTHROUGH clicked")}
                    resultsElementId={resultsId}
                />
                <ResultsPanel elementId={resultsId}>
                    {resultsComponents()}
                </ResultsPanel>
            </div>
        </div>;
    }
}