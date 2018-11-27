import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { getWalkthroughsCount, getCurrentWalkthroughIndex } from "../../../store/state/DemoState";
import { geWalkthroughUrls } from "../../../store/helpers/walkthroughUrls";

interface ProgressItemProps {
    active: boolean;
    url: string;
}

const ProgressItem = (props: ProgressItemProps) => {
    const { active, url } = props;
    const className = active
        ? "item active"
        : "item";

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
        
        return wtUrls.map((url, i) => <ProgressItem active={true} url={url} key={`done_${i}`} />);
    }

    toDoItems(start: number) {
        const { walkthroughUrls } = this.props;
        const wtUrls = walkthroughUrls ? walkthroughUrls.slice(start) : [];
        
        return wtUrls.map((url, i) => <ProgressItem active={false} url={url} key={`todo_${i}`} />);
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
        const currentWtIndex = getCurrentWalkthroughIndex(demos);
        const wtUrls = geWalkthroughUrls(demos);
        return {
            currentStep: currentWtIndex + 1,
            walkthroughUrls: wtUrls
        }
    }
)(WalkthroughProgressComponent);