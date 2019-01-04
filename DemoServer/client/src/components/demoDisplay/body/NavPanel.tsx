import * as React from "react";
import { AppState } from "../../../store/state";
import { DemoThunkDispatch } from "../../../store";
import { runDemo } from "../../../store/actions/demoActions";
import { connect } from "react-redux";
import { selectFirstWalkthroughUrl } from "../../../store/selectors/walkthroughUrls";
import { IconPlay, IconStudio, IconLearn } from "../../helpers/icons";

interface NavPanelStateProps {
    categorySlug: string;
    demoSlug: string;
    firstWtUrl?: string;
    studioUrl?: string;
    hideRunButton: boolean;
    hideWalkthroughButton: boolean;
}

interface NavPanelDispatchProps {
    onRunScriptClicked: () => void;
}

type NavPanelProps = NavPanelStateProps & NavPanelDispatchProps;

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
        return <button id="runScript" className="fab collapsed" type="button"
            onClick={() => this.handleRunScriptClick()}>
            <IconPlay /> Run script
        </button>;
    }

    render() {
        const { hideRunButton, hideWalkthroughButton } = this.props;
        return <div className="fab-container">
            {this.studioButton()}
            {!hideWalkthroughButton && this.walkthroughButton()}
            {!hideRunButton && this.runScriptButton()}
        </div>;
    }
}

function mapStateToProps({ demos }: AppState): NavPanelStateProps {
    const { categorySlug, demoSlug, demo } = demos;
    const nonInteractive = demo && demo.nonInteractive;
    const studioUrl = !nonInteractive && demo && demo.studioUrl;
    const conferenceMode = demo && demo.conferenceMode;

    return {
        categorySlug,
        demoSlug,
        firstWtUrl: selectFirstWalkthroughUrl(demos),
        studioUrl,
        hideRunButton: nonInteractive,
        hideWalkthroughButton: conferenceMode
    };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): NavPanelDispatchProps {
    return {
        onRunScriptClicked: () => dispatch(runDemo())
    };
}

export const NavPanel = connect<NavPanelStateProps, NavPanelDispatchProps, {}>(mapStateToProps, mapDispatchToProps)(NavPanelComponent);
