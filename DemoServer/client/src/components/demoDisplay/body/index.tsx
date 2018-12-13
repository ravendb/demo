import * as React from "react";
import * as classNames from "classnames";
import { Parameters, ParameterOwnProps } from "../parameters";
import { Code } from "../Code";
import { NavPanel } from "./NavPanel";
import { ResultsPanel } from "../results/ResultsPanel";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { WalkthroughOverlay } from "../walkthrough/WalkthroughOverlay";

export type DemoBodyOwnProps = ParameterOwnProps & {
    resultsComponents?: () => JSX.Element;
}

interface DemoBodyStateProps {
    showWalkthrough: boolean;
}

type DemoBodyProps = DemoBodyStateProps & DemoBodyOwnProps;

class DemoBodyComponent extends React.Component<DemoBodyProps, {}> {
    render() {
        const { resultsComponents, showWalkthrough } = this.props;

        const className = classNames({
            "walkthrough-active": showWalkthrough
        });

        return <div className="demo-body">
            <div id="demo-body-container" className={className}>
                {showWalkthrough && <WalkthroughOverlay />}
                <Parameters {...this.props} />
                <Code />
                <NavPanel />
                <ResultsPanel>
                    {resultsComponents && resultsComponents()}
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