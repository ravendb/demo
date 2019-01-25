import * as React from "react";
import { AssetIcon } from "../ui/AssetIcon";
import { AssetType } from "../../models/dtos/demo";
import { connect } from "react-redux";
import { Dispatch } from "redux";
import { goToDemoAssetPage } from "../../store/actions/navigation";

function isDemoType(type: AssetType): boolean {
    return type === "Demo";
}

export interface AssetsItem {
    url: string;
    title: string;
    type: AssetType;
}

interface AssetAnchorOwnProps {
    url: string;
    title: string;
    type: AssetType;
}

interface AssetAnchorDispatchProps {
    goToDemoPage?: () => void;
}

type AssetAnchorProps = AssetAnchorDispatchProps & AssetAnchorOwnProps;

const AssetAnchorComponent = (props: AssetAnchorProps) => {
    const { url, title, type, goToDemoPage } = props;

    const isDemoLink = isDemoType(type);

    return isDemoLink
        ? <a onClick={goToDemoPage}>{title}</a>
        : <a href={url} target="_blank">{title}</a>;
}

function mapDispatchToProps(dispatch: Dispatch, ownProps: AssetAnchorOwnProps): AssetAnchorDispatchProps {
    const { type, url } = ownProps;
    const isDemoLink = isDemoType(type);

    if (!isDemoLink) {
        return {};
    }

    return {
        goToDemoPage: () => dispatch(goToDemoAssetPage(url))
    };
}

const AssetAnchor = connect<{}, AssetAnchorDispatchProps, AssetAnchorOwnProps>(
    () => ({}),
    mapDispatchToProps
)(AssetAnchorComponent);

type AssetLinkProps = AssetAnchorOwnProps;

export const AssetLink = (props: AssetLinkProps) => <>
    <AssetIcon {...props} /> <AssetAnchor {...props} />
</>;
