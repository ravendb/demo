import * as React from "react";
import * as bsn from "bootstrap.native/dist/bootstrap-native-v4";
import { SelectDemoDropdown } from "./SelectDemoDropdown";
import { connect } from "react-redux";
import { goToMainPage } from "../../../store/actions/navigation";
import { AppState } from "../../../store/state";
import { selectIsOnMainPage } from "../../../store/selectors/router";
import { SettingsMenu } from "./settings/SettingsMenu";
import { DemoThunkDispatch } from "../../../store";

interface StateProps {
    isOnMainPage: boolean;
}

interface DispatchProps {
    goToMainPage: () => void;
}

type Props = StateProps & DispatchProps;

class HeaderComponent extends React.Component<Props, {}> {
    private dropdown: HTMLElement;

    public componentDidMount() {
        this.dropdown = document.getElementById("settings-dropdown");

        if (this.dropdown) {
            bsn.Dropdown(this.dropdown);
        }
    }

    public componentWillUnmount() {
        if (this.dropdown) {
            bsn.Dropdown(this.dropdown, "dispose");
        }
    }

    public render() {
        const { isOnMainPage, goToMainPage } = this.props;

        return <div className="header">
            <div>
                <a className="logo" onClick={goToMainPage}>
                    <img src="../img/logo.svg" alt="RavenDB Demo" />
                </a>
            </div>
            <div className="flex-grow"></div>
            {!isOnMainPage && <SelectDemoDropdown />}
            <div className="flex-grow"></div>
            <SettingsMenu />
        </div>;
    }
}

function mapStateToProps({ router }: AppState): StateProps {
    const isOnMainPage = selectIsOnMainPage(router);
    return { isOnMainPage };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): DispatchProps {
    return {
        goToMainPage: () => dispatch(goToMainPage())
    };
}

export const Header = connect<StateProps, DispatchProps, {}>(
    mapStateToProps,
    mapDispatchToProps
)(HeaderComponent);
