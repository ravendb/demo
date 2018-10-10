import * as React from "react";
import { Language } from "../../features/common/commonModels";
import { addHighlightHook, removeHighlightHook } from "../../utils/highlight";
import { addResizeListener, removeResizeListener } from "../../utils/resize";

const Prism = window["Prism"] as any;

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

interface CodePreviewState {
    windowWidth: number;
    windowHeight: number;
}

export class CodePreview extends React.Component<CodePreviewProps, CodePreviewState> {
    constructor(props) {
        super(props);

        this.state = {
            windowWidth: 0,
            windowHeight: 0
        };

        this.updateWindowDimensions = this.updateWindowDimensions.bind(this);
    }

    componentDidMount() {
        this.highlightSyntax();
        addHighlightHook();
        addResizeListener(this.updateWindowDimensions);
    }

    componentDidUpdate() {
        this.highlightSyntax();
    }

    componentWillUnmount() {
        removeResizeListener(this.updateWindowDimensions);
        removeHighlightHook();
    }

    updateWindowDimensions() {
        this.setState({
            windowWidth: window.innerWidth,
            windowHeight: window.innerHeight
        });
    }

    highlightSyntax() {
        const { id } = this.props;
        const element = document.getElementById(id);
        Prism.highlightAllUnder(element, false);
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