import * as React from "react";
import { Language } from "../../features/common/commonModels";
import { DivBounds, getBoundsRelatedToOtherElement } from "../../utils/divBounds";

function printDivBounds(bounds: DivBounds) {
    console.log("OFFSET LEFT: " + bounds.offsetLeft);
    console.log("OFFSET RIGHT: " + bounds.offsetRight);
    console.log("OFFSET TOP: " + bounds.offsetTop);
    console.log("OFFSET BOTTOM: " + bounds.offsetBottom);
}

const Prism = window["Prism"] as any;
Prism.hooks.add('complete', env => {
    console.log('env', env);
    const parentElement = env && env.element && env.element.parentElement;
    const lineHighlights = parentElement && parentElement.getElementsByClassName("line-highlight");
    const codeBody = document.getElementsByClassName("demo-body")[0];

    if (lineHighlights && lineHighlights.length > 0) {
        const firstHighlight = lineHighlights[0];
        const relativeBounds = getBoundsRelatedToOtherElement(firstHighlight as HTMLElement, codeBody as HTMLElement);
        printDivBounds(relativeBounds);

        console.log('code body', codeBody);
        console.log('firstHighlight', firstHighlight);
    }
});

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

export interface HighlightLinesRange {
    start: number;
    end: number;
}

export interface CodePreviewProps {
    id: string;
    language: Language;
    linesStart?: number;
    highlightLinesRange?: HighlightLinesRange;
}

export class CodePreview extends React.Component<CodePreviewProps, {}> {
    highlightSyntax() {
        const { id } = this.props;
        const element = document.getElementById(id);
        Prism.highlightAllUnder(element, false);
    }

    componentDidMount() {
        this.highlightSyntax();
    }

    componentDidUpdate() {
        this.highlightSyntax();
    }

    getHighlightRange() {
        const { highlightLinesRange } = this.props;
        return highlightLinesRange && highlightLinesRange.start && highlightLinesRange.end
            ? `${highlightLinesRange.start}-${highlightLinesRange.end}`
            : null;
    }

    render() {
        const { id, language, linesStart, children } = this.props;

        return <pre id={id} className="line-numbers" data-start={linesStart} data-line={this.getHighlightRange()}>
            <code className={`language-${language}`}>
                {children}
            </code>
        </pre>;
    }
}