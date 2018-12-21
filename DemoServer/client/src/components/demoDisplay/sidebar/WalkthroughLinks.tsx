import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { WalkthroughItem } from "./WalkthroughItem";
import { selectWalkthroughCount } from "../../../store/selectors/walkthroughs";

interface WalkthroughProps {
    itemsCount: number;
}

const getIndexedArray = (count: number) => [...Array(count).keys()];

class WalkthroughLinksComponent extends React.Component<WalkthroughProps> {

    renderWalkthroughItem = (index: number) => {        
        return <WalkthroughItem key={index} index={index} />;
    };
    
    render() {
        const { itemsCount } = this.props;
        
        return <>
            <h2>Walkthrough</h2>
            <hr />
            <ol className="walkthrough">
                {getIndexedArray(itemsCount).map(this.renderWalkthroughItem)}
            </ol>
        </>;
    }
}


export const WalkthroughLinks = connect<WalkthroughProps, {}, {}>(
    ({ demos }: AppState): WalkthroughProps => {
        const itemsCount = selectWalkthroughCount(demos);
        return {
            itemsCount
        };
    }
)(WalkthroughLinksComponent);