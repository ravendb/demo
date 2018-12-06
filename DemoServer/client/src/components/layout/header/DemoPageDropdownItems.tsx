import * as React from "react";
import { connect } from "react-redux";
import { shareDemo } from "../../../store/actions/demoActions";
import { DemoThunkDispatch } from "../../../store";

interface DispatchProps {
    shareDemo: () => void;
}

function DemoPageDropdownItemsComponent(props: DispatchProps) {
    const { shareDemo } = props;

    return <>
        <a className="dropdown-item" href="#">Reset demo</a>
        <a className="dropdown-item" href="#" onClick={shareDemo}>Share</a>
    </>;
}

export const DemoPageDropdownItems = connect<{}, DispatchProps, {}>(
    null,
    (dispatch: DemoThunkDispatch): DispatchProps => {
        return {
            shareDemo: () => dispatch(shareDemo())
        }
    }
)(DemoPageDropdownItemsComponent);