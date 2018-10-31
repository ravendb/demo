import * as React from "react";
import * as bsn from "bootstrap.native/dist/bootstrap-native-v4";
import { AppState } from "../../../store/state";
import { DemoAsyncDispatch } from "../../../store/async";
import { runDemo } from "../../../store/actions/demoActions";
import { connect } from "react-redux";

interface NavPanelOwnProps {
    onWalkthroughClick: () => void;
    resultsElementId?: string;
}

interface NavPanelStateProps {
}

interface NavPanelDispatchProps {
    onRunScriptClicked: () => void;
}

type NavPanelProps = NavPanelOwnProps & NavPanelStateProps & NavPanelDispatchProps;

class NavPanelComponent extends React.Component<NavPanelProps, {}> {
    collapseButton: HTMLElement;

    componentDidMount() {
        this.collapseButton = document.getElementById("runScript");
        this.collapseButton && bsn.Collapse(this.collapseButton);
    }

    componentWillUnmount() {
        this.collapseButton && bsn.Collapse(this.collapseButton, "dispose");
    }

    handleRunScriptClick() {
        const { onRunScriptClicked } = this.props;
        onRunScriptClicked();
        this.collapseButton && bsn.Collapse(this.collapseButton, "show");
    }

    walkthroughButton() {
        const { onWalkthroughClick } = this.props;
        return <button id="startWalkthrough" className="fab" type="button" onClick={onWalkthroughClick} >
            <i className="icon-learn"></i> Walkthrough
        </button>;
    }

    runScriptButton() {
        const { resultsElementId } = this.props;
        return <button id="runScript" className="fab collapsed" type="button"
            data-toggle="collapse" data-target={`#${resultsElementId}`}
            onClick={() => this.handleRunScriptClick()}>
            <i className="icon-play"></i> Run script
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
    return {
    };
}

function mapDispatchToProps(dispatch: DemoAsyncDispatch): NavPanelDispatchProps {
    return {
        onRunScriptClicked: () => dispatch(runDemo())
    };
}

export const NavPanel = connect<NavPanelStateProps, NavPanelDispatchProps, NavPanelOwnProps>(mapStateToProps, mapDispatchToProps)(NavPanelComponent);