import * as React from "react";

interface WalkthroughItem { 
    key: number;
    title: string;
    url: string;
}

export class WalkthroughItemComponent extends React.Component<WalkthroughItem> {

    render() {
        return <li key={this.props.key}><a href={this.props.url}>{this.props.title}</a></li>
    }
}
