import * as React from "react";
import { connect } from "react-redux";
import * as classNames from "classnames";
import { AppState } from "../../../store/state";
import { getCurrentWalkthroughIndex } from "../../../store/state/DemoState";

interface SelectedItem {
    selectedItemStep: number;
}

interface ListItem {
    listItemStep: number;
    title: string;
    url: string;
}

type Props = SelectedItem & ListItem;

class WalkthroughItemComponent extends React.Component<Props> {

    render() {
        const { selectedItemStep, listItemStep } = this.props;
        const isActive = selectedItemStep === listItemStep;

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

export const WalkthroughItem = connect<SelectedItem>(
    ({ demos }: AppState): SelectedItem => {
        return {
            selectedItemStep: getCurrentWalkthroughIndex(demos)
        }
    }
)(WalkthroughItemComponent);
