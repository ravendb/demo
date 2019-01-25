import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { Dialog, ButtonPanel, CancelButton, ReloadButton } from "./Dialog";
import { DemoThunkDispatch } from "../../../store";
import { closeResetDatabaseDialog, resetDatabase } from "../../../store/actions/settings";

interface StateProps {
    show: boolean;
    buttonsDisabled: boolean;
}

interface DispatchProps {
    onConfirm: () => void;
    onCancel: () => void;
}

type Props = StateProps & DispatchProps;

const ResetDatabaseConfirmComponent = (props: Props) => {
    const { show, onConfirm, onCancel, buttonsDisabled } = props;

    return <Dialog show={show}>
        <h2>Reset Demo Database</h2>
        <p>Are you sure you want to reset your demo database?</p>
        <p>Your demo progress won't be affected.</p>
        <ButtonPanel>
            <CancelButton onClick={onCancel} disabled={buttonsDisabled} />
            <ReloadButton text="Yes" onClick={onConfirm} disabled={buttonsDisabled} />
        </ButtonPanel>
    </Dialog>;
};

function mapStateToProps({ settings }: AppState): StateProps {
    const { showResetDatabaseDialog, resettingDatabase } = settings;

    return {
        show: showResetDatabaseDialog,
        buttonsDisabled: resettingDatabase
    };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): DispatchProps {
    return {
        onConfirm: () => dispatch(resetDatabase()),
        onCancel: () => dispatch(closeResetDatabaseDialog())
    };
}

export const ResetDatabaseConfirm = connect<StateProps, DispatchProps, {}>(
    mapStateToProps,
    mapDispatchToProps
)(ResetDatabaseConfirmComponent);
