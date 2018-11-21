import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { DemoWalkthroughDto } from "../../../models/dtos";
import { WalkthroughItemComponent } from "./WalkthroughItem";
import { createDemoWithWalkthroughPath } from "../../../utils/paths";

interface WalkthroughItem {
    slug: string;
    title: string;
}

interface WalkthroughProps {
    items: WalkthroughItem[];
    categorySlug: string;
    demoSlug: string;
}

class WalkthroughLinksComponent extends React.Component<WalkthroughProps> {

    renderWalkthroughItem = (item, index) => {        
        const url = createDemoWithWalkthroughPath({ category: this.props.categorySlug, demo: this.props.demoSlug, wtSlug: item.slug});        
        return <WalkthroughItemComponent key={index} title={item.title} url={url} />;
    };
    
    render() {
        const { items } = this.props;
        
        return <>
            <h2>Walkthrough</h2>
            <hr />
            <ol className="walkthrough">
                {items.map(this.renderWalkthroughItem)}
            </ol>
        </>;
    }
}

function walkthroughToLink(dto: DemoWalkthroughDto): WalkthroughItem {
    const { slug, title } = dto;
    return {
        slug,
        title
    };
}

export const WalkthroughLinks = connect<WalkthroughProps, {}, {}>(
    ({ demos }: AppState): WalkthroughProps => {
        const { demo: dto, categorySlug, demoSlug } = demos;
        const items = dto && dto.walkthroughs ? dto.walkthroughs.map(walkthroughToLink) : [];
        return {
            items,
            categorySlug,
            demoSlug
        };
    }
)(WalkthroughLinksComponent);