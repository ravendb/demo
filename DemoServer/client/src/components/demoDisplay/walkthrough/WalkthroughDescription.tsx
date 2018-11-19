import * as React from "react";
import { connect } from "react-redux";
import { RawHtml } from "../../helpers/RawHtml";
import { AppState } from "../../../store/state";
import { getCurrentWalkthrough } from "../../../store/state/DemoState";

interface Props {
    descriptionHtml: string;
    title: string;
    slug: string; 
}

function WalkthroughDescriptionComponent(props: Props) {
    const { descriptionHtml, title, slug } = props;
    
    const stepNumber = slug.split("-")[1];
    
    return <div className="walkthrough-step">
              <h2>Step {stepNumber} : {title}</h2>
              <h3><RawHtml>{descriptionHtml}</RawHtml></h3>
           </div>;
}

export const WalkthroughDescription = connect<Props>(
    ({ demos }: AppState): Props => {
        const wt = getCurrentWalkthrough(demos);
        return {
            descriptionHtml: wt && wt.descriptionHtml,
            title: wt && wt.title,
            slug: wt && wt.slug
        };
    }
)(WalkthroughDescriptionComponent);