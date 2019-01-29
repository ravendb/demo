import * as React from "react";

export class RawHtml extends React.Component<{}, {}> {
    private _rawInput() {
        const input = this.props.children as string;
        return { __html: input };
    }

    public render() {
        return <span dangerouslySetInnerHTML={this._rawInput()}></span>;
    }
}
