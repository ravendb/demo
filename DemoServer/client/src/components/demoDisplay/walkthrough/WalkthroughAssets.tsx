import * as React from "react";
import { connect } from "react-redux";
import { DemoAssetDto } from "../../../models/dtos";
import { AssetIcon } from "../../ui/AssetIcon";
import { AppState } from "../../../store/state";
import { getCurrentWalkthrough } from "../../../store/state/DemoState";

interface AssetProps {
    dto: DemoAssetDto;
}

class Asset extends React.Component<AssetProps, {}> {
    render() {
        const { dto } = this.props;
        if (!dto) {
            return null;
        }

        const { title, url } = dto;
        return <li>
            <AssetIcon {...dto} /> <a href={url} target="_blank">{title}</a>
        </li>;
    }
}

interface Props {
    assets: DemoAssetDto[];
}

function WalkthroughAssetsComponent(props: Props) {
    const { assets } = props;
    return <ul className="list-withIcons">
        {assets && assets.map((x, i) => <Asset dto={x} key={`wtasset_${i}`} />)}
    </ul>;
}

export const WalkthroughAssets = connect<Props>(
    ({ demos }: AppState): Props => {
        const wt = getCurrentWalkthrough(demos);
        return {
            assets: wt && wt.assets
        };
    }
)(WalkthroughAssetsComponent);