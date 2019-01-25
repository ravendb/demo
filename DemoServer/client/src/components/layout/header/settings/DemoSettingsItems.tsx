import * as React from "react";
import { connect } from "react-redux";
import { shareDemo } from "../../../../store/actions/demo";
import { DemoThunkDispatch } from "../../../../store";
import { showResetDatabasePopup } from "../../../../store/actions/settings";

interface DispatchProps {
    resetDatabase: () => void;
    shareDemo: () => void;
}

const DemoSettingsItemsComponent = (props: DispatchProps) => {
    const { resetDatabase, shareDemo } = props;

    return <>
        <a className="dropdown-item" onClick={shareDemo}>Share</a>
        <a className="dropdown-item" onClick={resetDatabase}>Reset Demo Database</a>
    </>;
};

export const DemoSettingsItems = connect<{}, DispatchProps, {}>(
    null,
    (dispatch: DemoThunkDispatch): DispatchProps => ({
        resetDatabase: () => dispatch(showResetDatabasePopup()),
        shareDemo: () => dispatch(shareDemo())
    })
)(DemoSettingsItemsComponent);
