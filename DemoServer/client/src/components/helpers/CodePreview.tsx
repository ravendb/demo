import * as React from "react";
import { Language, toHighlightLanguage } from "../../models/common";

const Prism = window["Prism"] as any;

interface CodePreviewProps {
    id: string;
    language: Language;
    linesStart?: number;
}

export class CodePreview extends React.Component<CodePreviewProps, {}> {
    public componentDidMount() {
        this._highlightSyntax();
    }

    public componentDidUpdate() {
        this._highlightSyntax();
    }

    private _highlightSyntax() {
        const { id } = this.props;
        const element = document.getElementById(id);
        Prism.highlightAllUnder(element, false);
    }

    public render() {
        const { id, language, linesStart, children } = this.props;
        const highlightLanguage = toHighlightLanguage(language);

        return <pre id={id} className="line-numbers" data-start={linesStart}>
            <code className={`language-${highlightLanguage}`}>
                {children}
            </code>
        </pre>;
    }
}
