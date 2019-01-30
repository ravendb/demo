import * as React from "react";
import { AssetIcon } from "../ui/AssetIcon";
import { AssetType } from "../../models/dtos/demo";

function isDemoType(type: AssetType): boolean {
    return type === "Demo";
}

export interface AssetsItem {
    url: string;
    title: string;
    type: AssetType;
}

interface AssetAnchorProps {
    url: string;
    title: string;
    type: AssetType;
}

export const AssetAnchor = (props: AssetAnchorProps) => {
    const { url, title, type } = props;

    const isDemoLink = isDemoType(type);

    return isDemoLink
        ? <a href={`/demos${url}`}>{title}</a>
        : <a href={url} target="_blank">{title}</a>;
};

type AssetLinkProps = AssetAnchorProps;

export const AssetLink = (props: AssetLinkProps) => <>
    <AssetIcon {...props} /> <AssetAnchor {...props} />
</>;
