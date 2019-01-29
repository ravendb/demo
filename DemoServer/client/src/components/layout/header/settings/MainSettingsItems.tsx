import * as React from "react";
import { connect } from "react-redux";
import { Dispatch } from "redux";
import { showResetDatabaseDialog, showResetProgressDialog } from "../../../../store/actions/settings";

interface DispatchProps {
    resetDatabase: () => void;
    resetUserProgress: () => void;
}

class MainSettingsItemsComponent extends React.Component<DispatchProps, {}> {

    public render() {
        const { resetDatabase, resetUserProgress } = this.props;

        return <>
            <a className="dropdown-item" onClick={resetDatabase}>Reset Demo Database</a>
            <a className="dropdown-item" onClick={resetUserProgress}>Mark all demos as not completed</a>
        </>;
    }
}

function mapDispatchToProps(dispatch: Dispatch): DispatchProps {
    return {
        resetDatabase: () => dispatch(showResetDatabaseDialog()),
        resetUserProgress: () => dispatch(showResetProgressDialog())
    };
}

export const MainSettingsItems = connect<{}, DispatchProps, {}>(
    () => ({}),
    mapDispatchToProps
)(MainSettingsItemsComponent);
