import * as React from "react";
import { AppState } from "../../../store/state";
import { DemoThunkDispatch } from "../../../store";
import { runDemo } from "../../../store/actions/demoActions";
import { connect } from "react-redux";
import { getFirstWalkthroughUrl } from "../../../store/helpers/walkthroughUrls";
import { IconPlay, IconStudio, IconLearn } from "../../helpers/icons";

interface NavPanelOwnProps {
    resultsElementId?: string;
}

interface NavPanelStateProps {
    categorySlug: string;
    demoSlug: string;
    firstWtUrl?: string;
    studioUrl?: string;
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

    studioButton() {
        const { studioUrl } = this.props;
        return studioUrl && <a href={studioUrl} id="openStudio" className="fab" target="_blank">
            <IconStudio /> Open in studio
        </a>;
    }

    walkthroughButton() {
        const { firstWtUrl } = this.props;
        return <a href={firstWtUrl} role="button" id="startWalkthrough" className="fab" >
            <IconLearn /> Walkthrough
        </a>;
    }

    runScriptButton() {
        const { resultsElementId } = this.props;

        return <button id="runScript" className="fab collapsed" type="button"
            data-toggle="collapse" data-target={`#${resultsElementId}`}
            onClick={() => this.handleRunScriptClick()}>
            <IconPlay /> Run script
        </button>;
    }

    render() {
        const { resultsElementId, hideRunButton } = this.props;
        return <div className="fab-container">
            {this.studioButton()}
            {this.walkthroughButton()}
            {resultsElementId && !hideRunButton && this.runScriptButton()}
        </div>;
    }
}

function mapStateToProps({ demos }: AppState): NavPanelStateProps {
    const { categorySlug, demoSlug, demo } = demos;
    const nonInteractive = demo && demo.nonInteractive;
    const studioUrl = !nonInteractive && demo && demo.studioUrl;
    return {
        categorySlug,
        demoSlug,
        firstWtUrl: getFirstWalkthroughUrl(demos),
        studioUrl,
        hideRunButton: nonInteractive
    };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): NavPanelDispatchProps {
    return {
        onRunScriptClicked: () => dispatch(runDemo())
    };
}

export const NavPanel = connect<NavPanelStateProps, NavPanelDispatchProps, NavPanelOwnProps>(mapStateToProps, mapDispatchToProps)(NavPanelComponent);
