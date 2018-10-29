import * as React from "react";

interface NavPanelProps {
    onWalkthroughClick: () => void;
    onRunScriptClicked: () => void;
    resultsElementId: string;
}

export class NavPanel extends React.Component<NavPanelProps, {}> {
    constructor(props) {
        super(props);
    }

    render() {
        const { onWalkthroughClick, onRunScriptClicked, resultsElementId } = this.props;
        return <div className="fab-container">
            <button id="startWalkthrough" className="fab" type="button" onClick={onWalkthroughClick} >
                <i className="icon-learn"></i> Walkthrough
            </button>
            <button id="runScript" className="fab collapsed" type="button"
                data-toggle="collapse" data-target={`#${resultsElementId}`}
                onClick={onRunScriptClicked}>
                <i className="icon-play"></i> Run script
            </button>
        </div>;
    }
}