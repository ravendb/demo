import * as React from "react";
import { connect } from "react-redux";
import { Dispatch } from "redux";
import { showResetDatabasePopup, showResetProgressPopup } from "../../../../store/actions/settings";

interface DispatchProps {
    resetDatabase: () => void;
    resetUserProgress: () => void;
}

class MainSettingsItemsComponent extends React.Component<DispatchProps, {}> {

    public render() {
        const { resetDatabase, resetUserProgress } = this.props;

        return <>
            <a className="dropdown-item" onClick={resetDatabase}>Reset Demo Database</a>
            <a className="dropdown-item" onClick={resetUserProgress}>Mark all demos as not run</a>
        </>;
    }
}

function mapDispatchToProps(dispatch: Dispatch): DispatchProps {
    return {
        resetDatabase: () => dispatch(showResetDatabasePopup()),
        resetUserProgress: () => dispatch(showResetProgressPopup())
    };
}

export const MainSettingsItems = connect<{}, DispatchProps, {}>(
    () => ({}),
    mapDispatchToProps
)(MainSettingsItemsComponent);
