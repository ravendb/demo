import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { RawHtml } from "../../helpers/RawHtml";
import { DemoAssetDto } from "../../../models/dtos";
import { getCurrentWalkthrough } from "../../../store/state/DemoState";

interface Props {
    descriptionHtml: string;
    assets: DemoAssetDto[];
}

class OverlayComponent extends React.Component<Props, {}> {
    render() {
        const { descriptionHtml } = this.props;
        return <>
            <div className="walkthrough-top"></div>
            <div id="demo-highlight">
                <div id="highlight-top"></div>
                <div id="highlight-right"></div>
                <div id="highlight-bottom">
                    <div className="walkthrough-description">
                        <h3><RawHtml>{descriptionHtml}</RawHtml></h3>
                    </div>
                </div>
                <div id="highlight-left"></div>
            </div>
        </>;
    }
}

export const WalkthroughOverlay = connect<Props>(
    ({ demos }: AppState): Props => {
        const wt = getCurrentWalkthrough(demos);
        return {
            descriptionHtml: wt && wt.descriptionHtml,
            assets: wt && wt.assets
        };
    }
)(OverlayComponent);