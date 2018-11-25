import * as React from "react";
import { connect } from "react-redux";
import {AppState} from "../../../store/state";
import {getCurrentWalkthroughIndex} from "../../../store/state/DemoState";

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
        return <li>
            <a className= {this.props.selectedItemStep === this.props.listItemStep ? "selectedItem" : ""} href={this.props.url}>
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
