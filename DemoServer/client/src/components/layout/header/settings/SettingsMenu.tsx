import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../../store/state";
import { selectIsOnMainPage } from "../../../../store/selectors/router";
import { IconSettings } from "../../../helpers/icons";
import { MainSettingsItems } from "./MainSettingsItems";
import { DemoSettingsItems } from "./DemoSettingsItems";

interface StateProps {
    isOnMainPage: boolean;
}

type Props = StateProps;

class SettingsMenuComponent extends React.Component<Props, {}> {

    public render() {
        const { isOnMainPage } = this.props;

        return <div id="settings-dropdown" className="dropdown">
            <a className="settings" data-toggle="dropdown">
                <IconSettings />
            </a>
            <div className="dropdown-menu placement-right">
                {isOnMainPage
                    ? <MainSettingsItems />
                    : <DemoSettingsItems />
                }
            </div>
        </div>;
    }
}

function mapStateToProps({ router }: AppState): StateProps {
    const isOnMainPage = selectIsOnMainPage(router);
    return { isOnMainPage };
}

export const SettingsMenu = connect<StateProps, {}>(
    mapStateToProps,
    () => ({})
)(SettingsMenuComponent);
