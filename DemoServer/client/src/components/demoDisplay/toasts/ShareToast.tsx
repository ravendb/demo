import * as React from "react";
import { connect } from "react-redux";
import { Toast, ToastProps, ToastStateProps, ToastDispatchProps } from "../../ui/Toast";
import { AppState } from "../../../store/state";
import { Dispatch } from "redux";
import { toggleDemoShareMessage } from "../../../store/actions/demo";

function ShareToastComponent(props: ToastProps) {
    return <Toast {...props}>Link has been copied to clipboard</Toast>;
}

export const ShareToast = connect<ToastStateProps, ToastDispatchProps, {}>(
    ({ demos }: AppState): ToastStateProps => ({
        show: demos.showShareMessage
    }),
    (dispatch: Dispatch): ToastDispatchProps => ({
        hideToast: () => dispatch(toggleDemoShareMessage(false))
    })
)(ShareToastComponent);
