import * as React from "react";
import { AssetType } from "../../models/dtos";
import { AssetIcon } from "../ui/AssetIcon";

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