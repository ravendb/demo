import * as React from "react";
import { AssetIcon } from "../ui/AssetIcon";
import { AssetType } from "../../models/dtos/demo";

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

const AssetAnchor = (props: AssetAnchorProps) => {
    const { url, title, type } = props;

    const isDemoLink = type === "Demo";

    const effectiveUrl = isDemoLink
        ? `/demos${url}`
        : url;

    return <a href={effectiveUrl} target={isDemoLink ? null : "_blank"}>{title}</a>;
}

type AssetLinkProps = AssetAnchorProps;

export const AssetLink = (props: AssetLinkProps) => <>
    <AssetIcon {...props} /> <AssetAnchor {...props} />
</>;