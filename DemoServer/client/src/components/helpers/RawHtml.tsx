import * as React from "react";

interface RawHtmlProps {
    className?: string;
}

export class RawHtml extends React.Component<RawHtmlProps, {}> {
    rawInput() {
        const input = this.props.children as string;
        return { __html: input };
    }

    render() {
        return <span dangerouslySetInnerHTML={this.rawInput()}></span>
    }
}