import * as React from "react";
import { AssetType, DemoAssetDto } from "../../../models/dtos";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { AssetIcon } from "../../ui/AssetIcon";

export interface AssetsItem {
    url: string;
    title: string;
    type: AssetType;
}

interface AssetsProps {
    items: AssetsItem[];
}

class AssetLinksComponent extends React.Component<AssetsProps, {}> {
    displayItem(item: AssetsItem, index: number) {
        const { url, title } = item;

        return <li key={`asset_${title}${index}`}>
            <AssetIcon {...item} /> <a href={url} target="_blank">{title}</a>
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

function assetToLink(dto: DemoAssetDto): AssetsItem {
    const { url, title, type } = dto;
    return { url, title, type };
}

export const AssetLinks = connect<AssetsProps>(
    ({ demos }: AppState): AssetsProps => {
        const dto = demos.demo;
        const items = dto && dto.assets ? dto.assets.map(assetToLink) : [];
        return {
            items
        };
    }
)(AssetLinksComponent);