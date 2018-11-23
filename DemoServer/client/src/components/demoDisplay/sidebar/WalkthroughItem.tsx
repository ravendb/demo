import * as React from "react";

interface WalkthroughItem {
    itemNum: number;
    title: string;
    url: string;
}

export class WalkthroughItemComponent extends React.Component<WalkthroughItem> {

    render() {
        return <li><a href={this.props.url}>{this.props.title}</a></li>
    }
}
