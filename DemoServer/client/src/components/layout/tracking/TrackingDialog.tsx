import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { giveTrackingConsent, withdrawTrackingConsent } from "../../../store/actions/tracking";
import { DemoThunkDispatch } from "../../../store";
import { IconSettings, IconLeft, IconConfirm } from "../../helpers/icons";

interface StateProps {
    show: boolean;
}

interface DispatchProps {
    onAccept: () => void;
    onWithdraw: () => void;
}

type Props = StateProps & DispatchProps;

interface State {
    showDetails: boolean;
    agreeChecked: boolean;
}

class TrackingDialogComponent extends React.Component<Props, State> {

    constructor(props: Props) {
        super(props);

        this.state = {
            showDetails: false,
            agreeChecked: true
        };

        this._handleOkClick = this._handleOkClick.bind(this);
        this._handleAdvancedClick = this._handleAdvancedClick.bind(this);
        this._handleBackClick = this._handleBackClick.bind(this);
        this._handleCheckboxToggle = this._handleCheckboxToggle.bind(this);
    }

    private _handleOkClick(): void {
        const { agreeChecked } = this.state;
        const { onAccept, onWithdraw } = this.props;

        if (agreeChecked) {
            onAccept();
        } else {
            onWithdraw();
        }
    }

    private _handleAdvancedClick(): void {
        this.setState({ showDetails: true });
    }

    private _handleBackClick(): void {
        this.setState({ showDetails: false });
    }

    private _handleCheckboxToggle(): void {
        const { agreeChecked } = this.state;
        this.setState({ agreeChecked: !agreeChecked });
    }

    private _okButton() {
        const { showDetails } = this.state;
        const wording = !showDetails ? "OK" : "Save & Close";

        return <button className="btn btn-primary" onClick={this._handleOkClick}>
            <IconConfirm /> <span><strong>{wording}</strong></span>
        </button>;
    }

    private _advancedButton() {
        return <button className="btn" onClick={this._handleAdvancedClick}>
            <IconSettings /> <span><strong>Advanced</strong></span>
        </button>;
    }

    private _backButton() {
        return <button className="btn btn-danger" onClick={this._handleBackClick}>
            <IconLeft /> Back
        </button>;
    }

    private _defaultContent() {
        return <>
            <h4>
                <i className="icon-shield margin-right margin-xs"></i> 
                <span>Privacy Settings</span>
            </h4>
            <hr />
            <p><strong>Dear User,</strong></p>
            <p>
                <small className="text-muted">
                    Before closing this window and proceeding to the website please review our{" "}
                    <a href="https://ravendb.net/terms" target="_blank"><strong>'Terms & Conditions'</strong></a>{" "}
                    and the{" "}
                    <a href="https://ravendb.net/privacy-policy" target="_blank">
                        <strong>'Privacy Policy'</strong>
                    </a> {" "}
                    for a better understanding of your rights and how we handle your personal data.
                </small>
            </p>
        </>;
    }

    private _advancedContent() {
        const { agreeChecked } = this.state;

        return <>
            <h4>
                <i className="icon-settings margin-right margin-xs" aria-hidden="true"></i> Advanced Settings
            </h4>
            <hr />
            <div className="checkbox">        
                <label htmlFor="cookie-bar-track-checkbox">
                    <input id="cookie-bar-track-checkbox" name="tracking-agree"
                        type="checkbox"
                        checked={agreeChecked}
                        onChange={this._handleCheckboxToggle} />
                    <small className="text-muted">
                        I agree to share my website usage data for marketing analysis purposes.
                    </small>
                </label>
            </div>
        </>;
    }

    public render() {
        const { show } = this.props;

        if (!show) {
            return null;
        }

        const { showDetails } = this.state;

        return <div className="cookie-bar fixed bottom">
            {showDetails ? this._advancedContent() : this._defaultContent()}

            <div className="flex-horizontal margin-top margin-sm">
                <div className="flex-grow"></div>
                <div className="margin-right margin-sm">
                    {showDetails ? this._backButton() : this._advancedButton()}
                </div>
                <div>
                    {this._okButton()}
                </div>
            </div>
        </div>;
    }
}

function mapStateToProps({ tracking }: AppState): StateProps {
    return {
        show: tracking.showConstentMonit
    };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): DispatchProps {
    return {
        onAccept: () => dispatch(giveTrackingConsent()),
        onWithdraw: () => dispatch(withdrawTrackingConsent())
    };
}

export const TrackingDialog = connect<StateProps, DispatchProps, {}>(
    mapStateToProps,
    mapDispatchToProps
)(TrackingDialogComponent);
