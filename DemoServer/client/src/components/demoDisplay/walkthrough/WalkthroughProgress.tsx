import * as React from "react";
import { connect } from "react-redux";
import * as classNames from "classnames";
import { AppState } from "../../../store/state";
import { selectWalkthroughUrls } from "../../../store/selectors/walkthroughUrls";
import { selectActiveWalkthroughIndex } from "../../../store/selectors/walkthroughs";

interface ProgressItemProps {
    active: boolean;
    url: string;
}

const ProgressItem = (props: ProgressItemProps) => {
    const { active, url } = props;

    const className = classNames("item", {
        "active": active
    });

    return <a href={url} className={className}></a>
}

interface Props {
    currentStep: number;
    walkthroughUrls: string[];
}

class WalkthroughProgressComponent extends React.Component<Props, {}> {
    doneItems(end: number) {
        const { walkthroughUrls } = this.props;
        const wtUrls = walkthroughUrls ? walkthroughUrls.slice(0, end) : [];
        
        return wtUrls.map(url => <ProgressItem active={true} url={url} key={`pritem_${url}`} />);
    }

    toDoItems(start: number) {
        const { walkthroughUrls } = this.props;
        const wtUrls = walkthroughUrls ? walkthroughUrls.slice(start) : [];
        
        return wtUrls.map(url => <ProgressItem active={false} url={url} key={`pritem_${url}`} />);
    }

    render() {
        const { currentStep } = this.props;

        return <div id="walkthroughProgress">
            {this.doneItems(currentStep)}
            {this.toDoItems(currentStep)}
        </div>;
    }
}

export const WalkthroughProgress = connect<Props>(
    ({ demos }: AppState): Props => {
        const currentWtIndex = selectActiveWalkthroughIndex(demos);
        const wtUrls = selectWalkthroughUrls(demos);
        return {
            currentStep: currentWtIndex + 1,
            walkthroughUrls: wtUrls
        }
    }
)(WalkthroughProgressComponent);