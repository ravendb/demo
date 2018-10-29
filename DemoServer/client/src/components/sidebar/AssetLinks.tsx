import * as React from "react";
import { AssetType } from "../../models/dtos";

export interface AssetsItem {
    href: string;
    title: string;
    type: AssetType;
}

interface AssetsProps {
    items: AssetsItem[];
}

export class AssetLinks extends React.Component<AssetsProps, {}> {
    typeToIcon(type: AssetType): string {
        switch (type) {
            case "Document":
                return "document";

            case "Downloadable":
                return "download";

            case "Link":
            default:
                return "link";
        }
    }

    displayItem(item: AssetsItem, index: number) {
        const { href, title, type } = item;
        const icon = this.typeToIcon(type);

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