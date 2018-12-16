import * as React from "react";
import { connect } from "react-redux";
import * as classNames from "classnames";
import { AppState } from "../../../store/state";
import { getPreviousWalkthroughUrl, getNextWalkthroughUrl, getUrlWithoutWalkthrough, getWalkthroughStepsCount } from "../../../store/helpers/walkthroughUrls";
import { WalkthroughProgress } from "./WalkthroughProgress";
import { NextButton, PreviousButton, CloseButton } from "./navButtons";

interface Props {
    previousUrl: string;
    nextUrl: string;
    closeUrl: string;
    stepsCount: number;
}

class WalkthroughNavComponent extends React.Component<Props, {}> {
    constructor(props) {
        super(props);
    }

    navButton(url: string, text: string, btnClass: string) {
        const className = classNames("walkthrough-nav", {
            "disabled": !url
        }, btnClass);

        return <a href={url || null} role="button" className={className}>{text}</a>;
    }

    render() {
        const { previousUrl, nextUrl, closeUrl, stepsCount } = this.props;

        return <div className="walkthrough-header">
            <h1><i className="icon-learn"></i> CODE WALKTHROUGH</h1>
            <div className="flex-grow"></div>            
            
            <div className="walkthrough-nav-container">
                { stepsCount > 1 &&
                    <>
                    < PreviousButton url={previousUrl} />
                    < WalkthroughProgress />
                    < NextButton url={nextUrl} />
                    </>
                }
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
        const stepsCount = getWalkthroughStepsCount(demos);
        return {
            nextUrl,
            previousUrl,
            closeUrl,
            stepsCount
        }
    }
)(WalkthroughNavComponent);