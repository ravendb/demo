import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { getCurrentWalkthrough } from "../../../store/state/DemoState";
import { Markdown } from "../../helpers/Markdown";
import { DemoLinkDto } from "../../../models/dtos";
import { IconRight } from "../../helpers/icons";
import { Header } from "../../ui/Header";

interface DemoLinkDisplayProps {
    url: string;
    title: string;
}

function DemoLinkDisplay(props: DemoLinkDisplayProps) {
    const { url, title } = props;
    const fullUrl = `demos/${url}`;
    return <>
        Learn more in <a href={fullUrl}>{title}</a>
    </>;
}

interface Props {
    descriptionHtml: string;
    title: string;
    slug: string;
    demoLink: DemoLinkDto;
}

function WalkthroughDescriptionComponent(props: Props) {
    const { descriptionHtml, title, slug, demoLink } = props;

    const stepNumber = slug && slug.split("-")[1];

    return <div className="walkthrough-step">
        <header>
            <h2> Step {stepNumber} : {title}</h2>
            <a href="" className="nextStep"> Next step <IconRight /></a>
        </header>
        <div className="description">
            {
                demoLink
                    ? <DemoLinkDisplay {...demoLink} />
                    : <Markdown>{descriptionHtml}</Markdown>
            }
        </div>
    </div>;
}

export const WalkthroughDescription = connect<Props>(
    ({ demos }: AppState): Props => {
        const wt = getCurrentWalkthrough(demos);

        if (!wt) {
            return {
                descriptionHtml: null,
                title: null,
                slug: null,
                demoLink: null
            }
        }
        const { descriptionHtml, title, slug, demoLink } = wt;

        return {
            descriptionHtml,
            title,
            slug,
            demoLink
        };
    }
)(WalkthroughDescriptionComponent);
