import * as React from "react";
import { Parameters, ParameterOwnProps } from "../Parameters";
import { Code } from "../Code";
import { NavPanel } from "./NavPanel";
import { ResultsPanel } from "../results/ResultsPanel";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { WalkthroughOverlay } from "../walkthrough/WalkthroughOverlay";

export type DemoBodyOwnProps = ParameterOwnProps & {
    resultsComponents: () => JSX.Element;
}

interface DemoBodyStateProps {
    showWalkthrough: boolean;
}

type DemoBodyProps = DemoBodyStateProps & DemoBodyOwnProps;

class DemoBodyComponent extends React.Component<DemoBodyProps, {}> {
    render() {
        const { paramDefinitions, resultsComponents, showWalkthrough } = this.props;
        const resultsId = "results";
        return <div className="demo-body">
            <div id="demo-body-container">
                {showWalkthrough && <WalkthroughOverlay />}
                {paramDefinitions && paramDefinitions.length > 0 && <Parameters paramDefinitions={paramDefinitions} />}
                <Code />
                <NavPanel resultsElementId={resultsId} />
                <ResultsPanel elementId={resultsId}>
                    {resultsComponents()}
                </ResultsPanel>
            </div>
        </div>;
    }
}

export const DemoBody = connect<DemoBodyStateProps, {}, DemoBodyOwnProps>(
    ({ demos }: AppState): DemoBodyStateProps => {
        return {
            showWalkthrough: !!demos.currentWalkthroughSlug
        };
    }
)(DemoBodyComponent);