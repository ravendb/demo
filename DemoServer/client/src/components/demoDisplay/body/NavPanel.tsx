import * as React from "react";
import { AppState } from "../../../store/state";
import { DemoThunkDispatch } from "../../../store";
import { runDemo } from "../../../store/actions/demoActions";
import { connect } from "react-redux";
import { getFirstWalkthroughUrl } from "../../../store/helpers/walkthroughUrls";

interface NavPanelOwnProps {
    resultsElementId?: string;
}

interface NavPanelStateProps {
    categorySlug: string;
    demoSlug: string;
    firstWtUrl?: string;
    settingPrerequisites: boolean;
    hideRunButton: boolean;
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
        const { firstWtUrl } = this.props;
        return <a href={firstWtUrl} role="button" id="startWalkthrough" className="fab" >
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
        const { resultsElementId, hideRunButton } = this.props;
        return <div className="fab-container">
            <a href="#" id="openStudio" className="fab">
                <i className="icon-studio"></i>
                Open in studio
            </a>
            {this.walkthroughButton()}
            {resultsElementId && !hideRunButton && this.runScriptButton()}
        </div>;
    }
}

function mapStateToProps({ demos }: AppState): NavPanelStateProps {
    const { categorySlug, demoSlug, settingPrerequisites, demo } = demos;
    const hideRunButton = demo && demo.nonInteractive;
    return {
        categorySlug,
        demoSlug,
        firstWtUrl: getFirstWalkthroughUrl(demos),
        settingPrerequisites,
        hideRunButton
    };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): NavPanelDispatchProps {
    return {
        onRunScriptClicked: () => dispatch(runDemo())
    };
}

export const NavPanel = connect<NavPanelStateProps, NavPanelDispatchProps, NavPanelOwnProps>(mapStateToProps, mapDispatchToProps)(NavPanelComponent);