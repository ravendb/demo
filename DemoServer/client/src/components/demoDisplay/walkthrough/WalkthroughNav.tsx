import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { getPreviousWalkthroughUrl, getNextWalkthroughUrl, getUrlWithoutWalkthrough } from "../../../store/helpers/walkthroughUrls";
import { WalkthroughProgress } from "./WalkthroughProgress";

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
            className += " disabled";
        }
        className += btnClass;

        return <a href={url || null} role="button" className={className}>{text}</a>;
    }

    previousButton() {
        const { previousUrl } = this.props;
        return this.navButton(previousUrl, "‹  Previous", "nav-prev");
    }

    nextButton() {
        const { nextUrl } = this.props;
        return this.navButton(nextUrl, "Next  ›", "nav-next");
    }

    closeButton() {
        const { closeUrl } = this.props;
        return this.navButton(closeUrl, "Close walkthrough   ×", "nav-close");
    }

    render() {
        return <div className="walkthrough-header">
        <h1><i className="icon-learn"></i> CODE WALKTHROUGH</h1>
        <div className="flex-grow"></div>
            <div className="walkthrough-nav-container">
            {this.previousButton()}
            <WalkthroughProgress />
            {this.nextButton()}
            {this.closeButton()}
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