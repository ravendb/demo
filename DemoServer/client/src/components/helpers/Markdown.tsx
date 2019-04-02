import * as React from "react";
import { RawHtml } from "./RawHtml";
import { getHtmlFromMarkdown } from "../../utils/codeParsing";

export class Markdown extends React.Component<{}, {}> {

    public render() {
        const { children } = this.props;
        if (!children) {
            return null;
        }

        const html = getHtmlFromMarkdown(children as string);
        return <RawHtml>{html}</RawHtml>;
    }
}
