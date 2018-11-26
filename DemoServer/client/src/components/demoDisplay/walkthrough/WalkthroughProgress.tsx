import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { getWalkthroughsCount, getCurrentWalkthroughIndex } from "../../../store/state/DemoState";

interface Props {
    currentStep: number;
    stepCount: number;
}

class WalkthroughProgressComponent extends React.Component<Props, {}> {
    getIndexedArray = (count: number) => count < 0 ? [] : [...Array(count).keys()];

    doneItem = (i: number) => <a href="" className="item active" key={`done_${i}`}></a>;
    toDoItem = (i: number) => <a href="" className="item" key={`todo_${i}`}></a>;

    doneItems(count: number) {
        return this.getIndexedArray(count).map(this.doneItem);
    }

    toDoItems(count: number) {
        return this.getIndexedArray(count).map(this.toDoItem);
    }

    render() {
        const { currentStep, stepCount } = this.props;
        const toDoCount = stepCount - currentStep;

        return <div id="walkthroughProgress">
            {this.doneItems(currentStep)}
            {this.toDoItems(toDoCount)}
        </div>;
    }
}

export const WalkthroughProgress = connect<Props>(
    ({ demos }: AppState): Props => {
        const wtCount = getWalkthroughsCount(demos);
        const currentWtIndex = getCurrentWalkthroughIndex(demos);
        return {
            currentStep: currentWtIndex + 1,
            stepCount: wtCount
        }
    }
)(WalkthroughProgressComponent);