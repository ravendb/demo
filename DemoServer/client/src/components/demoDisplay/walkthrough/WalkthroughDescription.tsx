import * as React from "react";
import { connect } from "react-redux";
import { RawHtml } from "../../helpers/RawHtml";
import { AppState } from "../../../store/state";
import { getCurrentWalkthrough } from "../../../store/state/DemoState";

interface Props {
    descriptionHtml: string;
}

function WalkthroughDescriptionComponent(props: Props) {
    const { descriptionHtml } = props;
    return <div className="walkthrough-description">
        <h3><RawHtml>{descriptionHtml}</RawHtml></h3>
    </div>;
}

export const WalkthroughDescription = connect<Props>(
    ({ demos }: AppState): Props => {
        const wt = getCurrentWalkthrough(demos);
        return {
            descriptionHtml: wt && wt.descriptionHtml
        };
    }
)(WalkthroughDescriptionComponent);