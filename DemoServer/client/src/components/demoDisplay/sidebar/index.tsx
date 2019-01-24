import * as React from "react";
import * as classNames from "classnames";
import { Controls } from "./Controls";
import { Heading } from "./Heading";
import { LanguageSelect } from "./LanguageSelect";
import { Description } from "./Description";
import { WalkthroughLinks } from "./WalkthroughLinks";
import { AssetLinks } from "./AssetLinks";
import { Language } from "../../../models/common";
import { AppState } from "../../../store/state";
import { connect } from "react-redux";

interface Props {
    title: string;
    selectedLanguage: Language;
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

        this.handleToggleCollapse = this.handleToggleCollapse.bind(this);
    }

    handleToggleCollapse() {
        this.setState({
            sidebarCollapsed: !this.state.sidebarCollapsed
        });
    }

    render() {
        const { title, selectedLanguage, conferenceMode } = this.props;
        const { sidebarCollapsed } = this.state;

        const sidebarClassName = classNames("sidebar", {
            "small": sidebarCollapsed || conferenceMode,
            "conference": conferenceMode
        });

        return <div id="sidebar" className={sidebarClassName}>
            <Controls toggleCollapse={!conferenceMode && this.handleToggleCollapse} />
            <div className="sidebar-body">
                <Heading text={title} />
                <LanguageSelect selected={selectedLanguage} />
                
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
    const { language, demo, conferenceMode } = demos;

    return {
        title: demo && demo.title,
        selectedLanguage: language,
        conferenceMode
    };
}

export const Sidebar = connect<Props>(mapStateToProps)(SidebarDisplay);