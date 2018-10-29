import * as React from "react";

export interface WalkthroughItem {
    href: string;
    title: string;
}

interface WalkthroughProps {
    items: WalkthroughItem[];
}

export class WalkthroughLinks extends React.Component<WalkthroughProps, {}> {
    displayItem(item: WalkthroughItem, index: number) {
        const { href, title } = item;
        return <li key={`walkthrough_${title}${index}`}><a href={href}>{title}</a></li>
    }

    render() {
        const { items } = this.props;
        return <>
            <h2>Walkthrough</h2>
            <hr />
            <ol className="walgrhough">
                {items.map((x, i) => this.displayItem(x, i))}
            </ol>
        </>;
    }
}