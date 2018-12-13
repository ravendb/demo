import * as React from "react";
import * as classNames from "classnames";
import { Controls } from "./Controls";
import { Heading } from "./Heading";
import { LanguageSelect } from "./LanguageSelect";
import { Description } from "./Description";
import { WalkthroughLinks } from "./WalkthroughLinks";
import { AssetLinks } from "./AssetLinks";
import { Language } from "../../../models/commonModels";
import { AppState } from "../../../store/state";
import { connect } from "react-redux";

interface SidebarProps {
    title: string;
    selectedLanguage: Language;
}

interface SidebarDisplayState {
    sidebarCollapsed: boolean;
}

export class SidebarDisplay extends React.Component<SidebarProps, SidebarDisplayState> {
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
        const { title, selectedLanguage } = this.props;
        const { sidebarCollapsed } = this.state;

        const sidebarClassName = classNames("sidebar", {
            "small": sidebarCollapsed
        });

        return <div id="sidebar" className={sidebarClassName}>
            <Controls toggleCollapse={this.handleToggleCollapse} />
            <div className="sidebar-body">
                <Heading text={title} />
                <LanguageSelect selected={selectedLanguage} />
                <Description />
                <WalkthroughLinks />
                <AssetLinks />
            </div>
        </div>;
    }
}

function mapStateToProps({ demos }: AppState): SidebarProps {
    const { language, demo } = demos;
    return {
        title: demo && demo.title,
        selectedLanguage: language
    };
}

export const Sidebar = connect<SidebarProps>(mapStateToProps)(SidebarDisplay);