import * as React from "react";
import * as bsn from "bootstrap.native/dist/bootstrap-native-v4";

interface NavPanelProps {
    onWalkthroughClick: () => void;
    onRunScriptClicked?: () => void;
    resultsElementId?: string;
}

export class NavPanel extends React.Component<NavPanelProps, {}> {
    collapseButton: HTMLElement;

    componentDidMount() {
        this.collapseButton = document.getElementById("runScript");
        this.collapseButton && bsn.Collapse(this.collapseButton);
    }

    componentWillUnmount() {
        this.collapseButton && bsn.Collapse(this.collapseButton, "dispose");
    }

    walkthroughButton() {
        const { onWalkthroughClick } = this.props;
        return <button id="startWalkthrough" className="fab" type="button" onClick={onWalkthroughClick} >
            <i className="icon-learn"></i> Walkthrough
        </button>;
    }

    runScriptButton() {
        const { onRunScriptClicked, resultsElementId } = this.props;
        <button id="runScript" className="fab collapsed" type="button"
            data-toggle="collapse" data-target={`#${resultsElementId}`}
            onClick={onRunScriptClicked}>
            <i className="icon-play"></i> Run script
        </button>;
    }

    render() {
        const { onRunScriptClicked, resultsElementId } = this.props;
        return <div className="fab-container">
            {this.walkthroughButton()}
            {onRunScriptClicked && resultsElementId && this.runScriptButton()}
        </div>;
    }
}