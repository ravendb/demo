import * as React from "react";
import * as markdowndeep from "markdowndeep";
import { RawHtml } from "./RawHtml";

export class Markdown extends React.Component<{}, {}> {
    private readonly markdown: any;

    constructor(props) {
        super(props);

        this.markdown = new markdowndeep.Markdown();
        this.markdown.ExtraMode = true;
        this.markdown.SafeMode = false;
    }

    render() {
        const { children } = this.props;
        if (!children) {
            return null;
        }

        const html = this.markdown.Transform(children);
        return <RawHtml>{html}</RawHtml>;
    }
}