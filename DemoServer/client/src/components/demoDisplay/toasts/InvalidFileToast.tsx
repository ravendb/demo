import * as React from "react";
import { connect } from "react-redux";
import { Toast, ToastProps, ToastStateProps, ToastDispatchProps } from "../../ui/Toast";
import { fileSizeLimitMB } from "../../../utils/fileUtils";
import { AppState } from "../../../store/state";
import { Dispatch } from "redux";
import { hideInvalidUploadMessage } from "../../../store/actions/parameters";

function InvalidFileToastComponent(props: ToastProps) {
    return <Toast {...props} className="danger">
        <i className="icon-warning"></i> This file cannot be uploaded. It is too large (more than {fileSizeLimitMB} MB).
    </Toast>;
}

export const InvalidFileToast = connect<ToastStateProps, ToastDispatchProps, {}>(
    ({ params }: AppState): ToastStateProps => ({
        show: params.showInvalidUploadMessage
    }),
    (dispatch: Dispatch): ToastDispatchProps => ({
        hideToast: () => dispatch(hideInvalidUploadMessage())
    })
)(InvalidFileToastComponent);
