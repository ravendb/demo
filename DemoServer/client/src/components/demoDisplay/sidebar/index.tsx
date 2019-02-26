import * as React from "react";
import * as classNames from "classnames";
import { Controls } from "./Controls";
import { Heading } from "./Heading";
import { LanguageSelect } from "./LanguageSelect";
import { Description } from "./Description";
import { WalkthroughLinks } from "./WalkthroughLinks";
import { AssetLinks } from "./AssetLinks";
import { AppState } from "../../../store/state";
import { connect } from "react-redux";

interface Props {
    conferenceMode: boolean;
}

interface State {
    sidebarCollapsed: boolean;
}

export class SidebarDisplay extends React.Component<Props, State> {
    constructor(props) {
        super(props);

        this.state = {
            sidebarCollapsed: false
        };

        this._handleToggleCollapse = this._handleToggleCollapse.bind(this);
    }

    private _handleToggleCollapse() {
        this.setState({
            sidebarCollapsed: !this.state.sidebarCollapsed
        });
    }

    public render() {
        const { conferenceMode } = this.props;
        const { sidebarCollapsed } = this.state;

        const sidebarClassName = classNames("sidebar", {
            "small": sidebarCollapsed || conferenceMode,
            "conference": conferenceMode
        });

        return <div id="sidebar" className={sidebarClassName}>
            <Controls toggleCollapse={!conferenceMode && this._handleToggleCollapse} />
            <div className="sidebar-body">
                <Heading />
                <LanguageSelect />

                {!conferenceMode && <>
                    <Description />
                    <WalkthroughLinks />
                    <AssetLinks />
                </>}
            </div>
        </div>;
    }
}

function mapStateToProps({ demos }: AppState): Props {
    const { conferenceMode } = demos;
    return { conferenceMode };
}

export const Sidebar = connect<Props>(mapStateToProps)(SidebarDisplay);