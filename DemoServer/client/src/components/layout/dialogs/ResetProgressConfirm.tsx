import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { Dialog, ButtonPanel, CancelButton, ReloadButton } from "./Dialog";
import { closeResetProgressDialog, resetProgress } from "../../../store/actions/settings";
import { DemoThunkDispatch } from "../../../store";

interface StateProps {
    show: boolean;
}

interface DispatchProps {
    onConfirm: () => void;
    onCancel: () => void;
}

type Props = StateProps & DispatchProps;

const ResetProgressConfirmComponent = (props: Props) => {
    const { show, onConfirm, onCancel } = props;

    return <Dialog show={show}>
        <h2>Mark All Demos as Not Run</h2>
        <p>
            Are you sure you want to mark all demos as not run?
            This will reset your demo progress.
        </p>
        <p>Your demo database won't be modified.</p>
        <ButtonPanel>
            <CancelButton onClick={onCancel} />
            <ReloadButton text="Yes" onClick={onConfirm} />
        </ButtonPanel>
    </Dialog>;
};

function mapStateToProps({ settings }: AppState): StateProps {
    return {
        show: settings.showResetProgressDialog
    };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): DispatchProps {
    return {
        onConfirm: () => dispatch(resetProgress()),
        onCancel: () => dispatch(closeResetProgressDialog())
    };
}

export const ResetProgressConfirm = connect<StateProps, DispatchProps, {}>(
    mapStateToProps,
    mapDispatchToProps
)(ResetProgressConfirmComponent);
