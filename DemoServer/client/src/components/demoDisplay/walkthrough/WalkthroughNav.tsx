import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { getPreviousWalkthroughUrl, getNextWalkthroughUrl, getUrlWithoutWalkthrough } from "../../../store/helpers/walkthroughUrls";

interface Props {
    previousUrl: string;
    nextUrl: string;
    closeUrl: string;
}

class WalkthroughNavComponent extends React.Component<Props, {}> {
    constructor(props) {
        super(props);
    }

    previousButton() {
        const { previousUrl } = this.props;
        return previousUrl && <a href={previousUrl} role="button" className="fab">Previous</a>;
    }

    nextButton() {
        const { nextUrl } = this.props;
        return nextUrl && <a href={nextUrl} role="button" className="fab">Next</a>;
    }

    closeButton() {
        const { closeUrl } = this.props;
        return <a href={closeUrl} role="button" className="fab">Close</a>;
    }

    render() {
        return <div>
            {this.previousButton()}<br />
            {this.nextButton()}<br /><br />
            {this.closeButton()}
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