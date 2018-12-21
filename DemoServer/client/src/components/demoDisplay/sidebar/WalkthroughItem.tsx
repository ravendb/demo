import * as React from "react";
import { createSelector } from "reselect"
import * as classNames from "classnames";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { DemoState } from "../../../store/state/DemoState";
import { selectWalkthroughUrls } from "../../../store/selectors/walkthroughUrls";

interface StateProps {
    title: string;
    url: string;
    isActive: boolean;
}

interface OwnProps {
    index: number;
}

type Props = StateProps & OwnProps;

class WalkthroughItemComponent extends React.Component<Props> {

    render() {
        const { isActive } = this.props;

        const className = classNames({
            "active": isActive
        });

        return <li className={className}>
            <a href={this.props.url}>
                {this.props.title}
            </a>
        </li>
    }
}

const selectWalkthrough = (state: DemoState, ownProps: OwnProps): StateProps => {
    const { demo } = state;
    const { index } = ownProps;
    const wt = demo && demo.walkthroughs && demo.walkthroughs[index];

    if (!wt) {
        return null;
    }

    const urls = selectWalkthroughUrls(state);
    return {
        isActive: wt.isActive,
        title: wt.title,
        url: urls[index]
    }
};

const makeSelectWalkthrough = () => {
    return createSelector(
        [selectWalkthrough],
        (walkthrough) => walkthrough
    )
};

const makeMapStatetoProps = () => {
    const getWalkthrough = makeSelectWalkthrough();
    const mapStateToProps = ({ demos }: AppState, props: OwnProps): StateProps => {
        return getWalkthrough(demos, props);
    }
    return mapStateToProps;
}

export const WalkthroughItem = connect<StateProps, {}, OwnProps>(makeMapStatetoProps)(WalkthroughItemComponent);