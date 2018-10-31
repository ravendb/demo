import * as React from "react";
import { addHighlightHook, removeHighlightHook } from "../../utils/highlight";
import { addResizeListener, removeResizeListener } from "../../utils/resize";
import { Language } from "../../models/commonModels";
import { LinesRangeDto } from "../../models/dtos";

const Prism = window["Prism"] as any;

export type HighlightLinesRange = LinesRangeDto;

interface CodeHighlightProps {
    id: string;
    language: Language;
    linesStart?: number;
    highlightLinesRange: HighlightLinesRange;
}

interface CodeHighlightState {
    windowWidth: number;
    windowHeight: number;
}

export class CodeHighlight extends React.Component<CodeHighlightProps, CodeHighlightState> {
    constructor(props) {
        super(props);

        this.state = {
            windowWidth: 0,
            windowHeight: 0
        };

        this.updateWindowDimensions = this.updateWindowDimensions.bind(this);
    }

    componentDidMount() {
        addHighlightHook();
        this.highlightSyntax();
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