import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { getPreviousWalkthroughUrl, getNextWalkthroughUrl, getUrlWithoutWalkthrough } from "../../../store/helpers/walkthroughUrls";
import { WalkthroughProgress } from "./WalkthroughProgress";
import { NextButton, PreviousButton, CloseButton } from "./navButtons";

interface Props {
    previousUrl: string;
    nextUrl: string;
    closeUrl: string;
}

class WalkthroughNavComponent extends React.Component<Props, {}> {
    constructor(props) {
        super(props);
    }

    navButton(url: string, text: string, btnClass: string) {
        let className = "walkthrough-nav ";
        if (!url) {
            className += "disabled ";
        }
        className += btnClass;

        return <a href={url || null} role="button" className={className}>{text}</a>;
    }

    render() {
        const { previousUrl, nextUrl, closeUrl } = this.props;

        return <div className="walkthrough-header">
            <h1><i className="icon-learn"></i> CODE WALKTHROUGH</h1>
            <div className="flex-grow"></div>
            <div className="walkthrough-nav-container">
                <PreviousButton url={previousUrl} />
                <WalkthroughProgress />
                <NextButton url={nextUrl} />
                <CloseButton url={closeUrl} />
            </div>
        </div>;
    }
}

export const WalkthroughNav = connect<Props>(
    ({ demos }: AppState): Props => {
        const previousUrl = getPreviousWalkthroughUrl(demos);
        const nextUrl = getNextWalkthroughUrl(demos);
        const closeUrl = getUrlWithoutWalkthrough(demos);
        return {
            nextUrl,
            previousUrl,
            closeUrl
        }
    }
)(WalkthroughNavComponent);