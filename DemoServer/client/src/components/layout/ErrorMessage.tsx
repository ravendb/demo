import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../store/state";
import { Dispatch } from "redux";
import { IconReload } from "../helpers/icons";
import { reloadOnApiError } from "../../store/actions/errorActions";

interface StateProps {
    show: boolean;
}

interface DispatchProps {
    onReload: () => void;
}

type Props = StateProps & DispatchProps;

const ErrorMessageDisplay = (props: Props) => {
    const { show, onReload } = props;

    return show && <div id="errorMessage">
        <div className="message">
            <h2>Oops... :(</h2>
            <p>An error occurred, please reload and try again.</p>
            <div className="text-center margin-top">
                <button className="btn btn-default" onClick={onReload}>
                    <IconReload /> Reload
                </button>
            </div>
        </div>
        <div className="overlay"></div>
    </div>;
}

export const ErrorMessage = connect<StateProps, DispatchProps, {}>(
    ({ error }: AppState): StateProps => ({
        show: !!error.error
    }),
    (dispatch: Dispatch): DispatchProps => ({
        onReload: () => dispatch(reloadOnApiError())
    })
)(ErrorMessageDisplay);