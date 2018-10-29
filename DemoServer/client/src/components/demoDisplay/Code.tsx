import * as React from "react";
import { Language } from "../../models/commonModels";
import { Usings } from "./Usings";
import { CodePreview } from "../helpers/CodePreview";
import { HighlightLinesRange, CodeHighlight } from "../helpers/CodeHighlight";

interface CodeProps {
    language: Language;
    usings: string;
    sourceCode: string;
    highlightLinesRange?: HighlightLinesRange;
}

export class Code extends React.Component<CodeProps, {}> {
    render() {
        const { language, usings, sourceCode, highlightLinesRange } = this.props;
        const usingsLinesCnt = usings ? usings.split("\n").length : 0;
        const linesStart = usingsLinesCnt + 1;

        const code = highlightLinesRange
            ? <CodeHighlight id="preview-source-code" {...this.props} linesStart={linesStart} highlightLinesRange={highlightLinesRange}>
                {sourceCode}
            </CodeHighlight>
            : <CodePreview id="preview-source-code" {...this.props} linesStart={linesStart}>
                {sourceCode}
            </CodePreview>;

        return <div className="demo-code">
            <Usings language={language}>{usings}</Usings>
            {code}
        </div>;
    }
}