import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { Dispatch } from "redux";
import { reloadOnApiError } from "../../../store/actions/error";
import { Dialog, ReloadButton, ButtonPanel } from "./Dialog";

interface StateProps {
    show: boolean;
}

interface DispatchProps {
    onReload: () => void;
}

type Props = StateProps & DispatchProps;

const ErrorMessageComponent = (props: Props) => {
    const { show, onReload } = props;

    return <Dialog show={show}>
        <h2>Oops... :(</h2>
        <p>An error occurred, please reload and try again.</p>
        <ButtonPanel>
            <ReloadButton onClick={onReload}/>
        </ButtonPanel>
    </Dialog>;
};

export const ErrorMessage = connect<StateProps, DispatchProps, {}>(
    ({ error }: AppState): StateProps => ({
        show: !!error.error
    }),
    (dispatch: Dispatch): DispatchProps => ({
        onReload: () => dispatch(reloadOnApiError())
    })
)(ErrorMessageComponent);
