import * as React from "react";
import { Controls } from "./Controls";
import { Heading } from "./Heading";
import { LanguageSelect } from "./LanguageSelect";
import { Description } from "./Description";
import { WalkthroughLinks, WalkthroughItem } from "./WalkthroughLinks";
import { AssetLinks, AssetsItem } from "./AssetLinks";
import { Language } from "../../models/commonModels";
import { AppState } from "../../store/state";
import { DemoWalkthroughDto, DemoAssetDto } from "../../models/dtos";
import { DemoAsyncDispatch } from "../../store/async";
import { connect } from "react-redux";

export interface SidebarOwnProps {
    title: string;
    description: React.ReactNode;
}

interface SidebarStateProps {
    selectedLanguage: Language;
    walkthroughLinks: WalkthroughItem[];
    assetLinks: AssetsItem[];
}

interface SidebarDispatchProps {
}

export type SidebarProps = SidebarOwnProps & SidebarStateProps & SidebarDispatchProps;

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
        const { title, description, walkthroughLinks, assetLinks, selectedLanguage } = this.props;
        const { sidebarCollapsed } = this.state;
        const customClass = sidebarCollapsed ? "small" : "";

        return <div id="sidebar" className={`sidebar ${customClass}`}>
            <Controls toggleCollapse={this.handleToggleCollapse} />
            <div className="sidebar-body">
                <Heading text={title} />
                <LanguageSelect selected={selectedLanguage} />
                <Description>{description}</Description>
                <WalkthroughLinks items={walkthroughLinks} />
                <AssetLinks items={assetLinks} />
            </div>
        </div>;
    }
}

function walkthroughToLink(dto: DemoWalkthroughDto): WalkthroughItem {
    return {
        href: dto.slug,
        title: dto.title
    };
}

function assetToLink(dto: DemoAssetDto): AssetsItem {
    return {
        href: dto.url,
        title: dto.title,
        type: dto.type
    };
}

function mapStateToProps({ demos }: AppState): SidebarStateProps {
    const { demo: dto, language } = demos;
    const walkthroughLinks = dto && dto.walkthroughs ? dto.walkthroughs.map(walkthroughToLink) : [];
    const assetLinks = dto && dto.assets ? dto.assets.map(assetToLink) : [];

    return {
        selectedLanguage: language,
        walkthroughLinks,
        assetLinks
    };
}

function mapDispatchToProps(dispatch: DemoAsyncDispatch): SidebarDispatchProps {
    return {
    };
}

export const Sidebar = connect<SidebarStateProps, SidebarDispatchProps, SidebarOwnProps>(mapStateToProps, mapDispatchToProps)(SidebarDisplay);