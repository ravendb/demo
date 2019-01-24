import * as React from "react";
import { Language } from "../../models/common";

const Prism = window["Prism"] as any;

interface CodePreviewProps {
    id: string;
    language: Language;
    linesStart?: number;
}

export class CodePreview extends React.Component<CodePreviewProps, {}> {
    componentDidMount() {
        this.highlightSyntax();
    }

    componentDidUpdate() {
        this.highlightSyntax();
    }

    highlightSyntax() {
        const { id } = this.props;
        const element = document.getElementById(id);
        Prism.highlightAllUnder(element, false);
    }

    render() {
        const { id, language, linesStart, children } = this.props;

        return <pre id={id} className="line-numbers" data-start={linesStart}>
            <code className={`language-${language}`}>
                {children}
            </code>
        </pre>;
    }
}