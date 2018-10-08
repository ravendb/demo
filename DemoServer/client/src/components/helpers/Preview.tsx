import * as React from "react";
import { Language } from "../../features/common/commonModels";

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

interface CodePreviewProps {
    language: Language;
    linesStart?: number;
}

export class CodePreview extends React.Component<CodePreviewProps, {}> {
    render() {
        const { language, linesStart, children } = this.props;

        return <pre className="line-numbers" data-start={linesStart}>
            <code className={`language-${language}`}>
                {children}
            </code>
        </pre>;
    }
}