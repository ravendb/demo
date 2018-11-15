import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { DemoWalkthroughDto } from "../../../models/dtos";
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

class WalkthroughLinksComponent extends React.Component<WalkthroughProps, {}> {
    displayItem(item: WalkthroughItem, index: number) {
        const { categorySlug, demoSlug } = this.props;
        const { slug, title } = item;
        const url = createDemoWithWalkthroughPath({
            category: categorySlug,
            demo: demoSlug,
            wtSlug: slug
        });
        return <li key={`walkthrough_${title}${index}`}><a href={url}>{title}</a></li>
    }

    render() {
        const { items } = this.props;
        return <>
            <h2>Walkthrough</h2>
            <hr />
            <ol className="walkthrough">
                {items.map((x, i) => this.displayItem(x, i))}
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