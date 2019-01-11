import * as React from "react";
import { connect } from "react-redux";
import { DemoAssetDto } from "../../../models/dtos";
import { AppState } from "../../../store/state";
import { selectActiveWalkthrough } from "../../../store/selectors/walkthroughs";
import { AssetLink } from "../../helpers/AssetLink";

interface AssetProps {
    dto: DemoAssetDto;
}

class Asset extends React.Component<AssetProps, {}> {
    render() {
        const { dto } = this.props;

        return <li>
            <AssetLink {...dto} />
        </li>;
    }
}

interface Props {
    assets: DemoAssetDto[];
}

function WalkthroughAssetsComponent(props: Props) {
    const { assets } = props;
    if (assets && !assets.length) {
        return null;
    }
    
    return <div className="walkthrough-assets"> 
              <h3>Related Links:</h3> 
              <ul className="list-withIcons">
                 {assets && assets.map((x, i) => <Asset dto={x} key={`wtasset_${i}`} />)}
              </ul>
           </div>
}

export const WalkthroughAssets = connect<Props>(
    ({ demos }: AppState): Props => {
        const wt = selectActiveWalkthrough(demos);
        return {
            assets: wt && wt.assets
        };
    }
)(WalkthroughAssetsComponent);
