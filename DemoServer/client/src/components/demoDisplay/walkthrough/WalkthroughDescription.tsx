import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { Markdown } from "../../helpers/Markdown";
import { IconRight, IconCancel } from "../../helpers/icons";
import { selectUrlWithoutWalkthrough, selectNextWalkthroughUrl } from "../../../store/selectors/walkthroughUrls";
import { selectActiveWalkthrough, selectWalkthroughCount } from "../../../store/selectors/walkthroughs";
import { DemoLinkDto } from "../../../models/dtos/demo";

interface DemoLinkDisplayProps {
    url: string;
    title: string;
}

function DemoLinkDisplay(props: DemoLinkDisplayProps) {
    const { url, title } = props;
    const fullUrl = `demos/${url}`;
    return <>
        See step explanation in <a href={fullUrl}>{title}</a>
    </>;
}

interface Props {
    descriptionHtml: string;
    title: string;
    slug: string;
    demoLink: DemoLinkDto;
    nextStepUrl: string;
    numberOfSteps: number;
    closeUrl: string;
}

function WalkthroughDescriptionComponent(props: Props) {
    const { descriptionHtml, title, slug, demoLink, nextStepUrl, numberOfSteps, closeUrl } = props;

    const stepNumber = slug && slug.split("-")[1];

    return <div className="walkthrough-step">
        <header>
            <h2> Step {stepNumber} : {title}</h2>
        </header>
        <div className="description">
            {
                demoLink
                    ? <DemoLinkDisplay {...demoLink} />
                    : <Markdown>{descriptionHtml}</Markdown>
            }
        </div>
        <footer>
            {
                stepNumber === numberOfSteps.toString()
                    ? <a href={closeUrl} className="nextStep"> Close <IconCancel /></a>
                    : <a href={nextStepUrl} className="nextStep"> Next Step <IconRight /></a>
            }
        </footer>
    </div>;
}

export const WalkthroughDescription = connect<Props>(
    ({ demos }: AppState): Props => {
        const wt = selectActiveWalkthrough(demos);

        const nextStepUrl = selectNextWalkthroughUrl(demos);
        const numberOfSteps = selectWalkthroughCount(demos);
        const closeUrl = selectUrlWithoutWalkthrough(demos);

        if (!wt) {
            return {
                descriptionHtml: null,
                title: null,
                slug: null,
                demoLink: null,
                nextStepUrl: null,
                numberOfSteps: 0,
                closeUrl: null
            }
        }
        const { descriptionHtml, title, slug, demoLink } = wt;

        return {
            descriptionHtml,
            title,
            slug,
            demoLink,
            nextStepUrl,
            numberOfSteps,
            closeUrl
        }
    }
)(WalkthroughDescriptionComponent);
