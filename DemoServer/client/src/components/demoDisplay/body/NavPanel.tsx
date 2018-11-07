import * as React from "react";
import { AppState } from "../../../store/state";
import { DemoThunkDispatch } from "../../../store";
import { runDemo } from "../../../store/actions/demoActions";
import { connect } from "react-redux";
import { createDemoWithWalkthroughPath } from "../../../utils/paths";

interface NavPanelOwnProps {
    resultsElementId?: string;
}

interface NavPanelStateProps {
    categorySlug: string;
    demoSlug: string;
    firstWtSlug?: string;
    settingPrerequisites: boolean;
}

interface NavPanelDispatchProps {
    onRunScriptClicked: () => void;
}

type NavPanelProps = NavPanelOwnProps & NavPanelStateProps & NavPanelDispatchProps;

class NavPanelComponent extends React.Component<NavPanelProps, {}> {
    handleRunScriptClick() {
        const { onRunScriptClicked } = this.props;
        onRunScriptClicked();
    }

    walkthroughButton() {
        const { categorySlug, demoSlug, firstWtSlug } = this.props;
        const url = createDemoWithWalkthroughPath({
            category: categorySlug,
            demo: demoSlug,
            wtSlug: firstWtSlug
        });
        return <a href={url} role="button" id="startWalkthrough" className="fab" >
            <i className="icon-learn"></i> Walkthrough
        </a>;
    }

    runScriptButton() {
        const { resultsElementId, settingPrerequisites } = this.props;
        const wording = settingPrerequisites ? "Setting prerequisites..." : "Run script";

        return <button id="runScript" className="fab collapsed" type="button"
            disabled={settingPrerequisites}
            data-toggle="collapse" data-target={`#${resultsElementId}`}
            onClick={() => this.handleRunScriptClick()}>
            <i className="icon-play"></i> {wording}
        </button>;
    }

    render() {
        const { resultsElementId } = this.props;
        return <div className="fab-container">
            {this.walkthroughButton()}
            {resultsElementId && this.runScriptButton()}
        </div>;
    }
}

function mapStateToProps({ demos }: AppState): NavPanelStateProps {
    const { categorySlug, demoSlug, demo, settingPrerequisites } = demos;
    const firstWt = demo && demo.walkthroughs
        && demo.walkthroughs.length > 0
        && demo.walkthroughs[0];
    return {
        categorySlug,
        demoSlug,
        firstWtSlug: firstWt && firstWt.slug,
        settingPrerequisites
    };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): NavPanelDispatchProps {
    return {
        onRunScriptClicked: () => dispatch(runDemo())
    };
}

export const NavPanel = connect<NavPanelStateProps, NavPanelDispatchProps, NavPanelOwnProps>(mapStateToProps, mapDispatchToProps)(NavPanelComponent);