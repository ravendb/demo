import * as React from "react";
import { IconLeft, IconCollapse, IconExpand } from "../../helpers/icons";
import { Dispatch } from "redux";
import { connect } from "react-redux";
import { goToMainPage } from "../../../store/actions/navigationActions";

interface DispatchProps {
    goToMainPage: () => void;
}

interface OwnProps {
    toggleCollapse?: () => void;
}

type Props = DispatchProps & OwnProps;

function ControlsComponent(props: Props) {
    const { toggleCollapse, goToMainPage } = props;

    return <div className="sidebar-controlls">
        {toggleCollapse &&
            <button id="collapseSidebar" className="collapse-sidebar" onClick={toggleCollapse}>
                <IconCollapse />
                <IconExpand />
            </button>
        }
        <a onClick={goToMainPage} className="back-button"><IconLeft /></a>
    </div>;
}

function mapDispatchToProps(dispatch: Dispatch): DispatchProps {
    return {
        goToMainPage: () => dispatch(goToMainPage())
    };
}

export const Controls = connect<{}, DispatchProps, OwnProps>(
    () => ({}),
    mapDispatchToProps
)(ControlsComponent);