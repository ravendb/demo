import * as React from "react";
import * as bsn from "bootstrap.native/dist/bootstrap-native-v4";
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
    const { categorySlug, demoSlug, demo } = demos;
    const firstWt = demo && demo.walkthroughs
        && demo.walkthroughs.length > 0
        && demo.walkthroughs[0];
    return {
        categorySlug,
        demoSlug,
        firstWtSlug: firstWt && firstWt.slug
    };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): NavPanelDispatchProps {
    return {
        onRunScriptClicked: () => dispatch(runDemo())
    };
}

export const NavPanel = connect<NavPanelStateProps, NavPanelDispatchProps, NavPanelOwnProps>(mapStateToProps, mapDispatchToProps)(NavPanelComponent);