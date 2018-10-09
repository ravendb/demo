import * as React from "react";

interface AssetsItem {
    icon: string;
    href: string;
    title: string;
}

interface AssetsProps {
    items: AssetsItem[];
}

export class Assets extends React.Component<AssetsProps, {}> {
    displayItem(item: AssetsItem, index: number) {
        const { icon, href, title } = item;
        return <li key={`asset_${title}${index}`}>
            <i className={`icon-${icon}`}></i> <a href={href}>{title}</a>
        </li>;
    }

    render() {
        const { items } = this.props;
        return <>
            <h2>Assets</h2>
            <hr />
            <ul className="list-withIcons">
                {items.map((x, i) => this.displayItem(x, i))}
            </ul>
        </>;
    }
}