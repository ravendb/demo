import * as React from "react";
import { Language } from "../../models/commonModels";
import { Usings } from "./Usings";
import { CodePreview } from "../helpers/CodePreview";
import { CodeHighlight } from "../helpers/CodeHighlight";
import { sliceIntoTwo } from "../../utils/codeParsing";
import { AppState } from "../../store/state";
import { connect } from "react-redux";
import { getCurrentWalkthrough } from "../../store/state/DemoState";
import { LinesRangeDto } from "../../models/dtos";

export interface CodeProps {
    language: Language;
    sourceCode: string;
    highlightLinesRange?: LinesRangeDto;
    usingsLastLine: number;
}

export class CodeDisplay extends React.Component<CodeProps, {}> {
    render() {
        const { language, usingsLastLine, sourceCode, highlightLinesRange } = this.props;
        if (!sourceCode) {
            return null;
        }

        const [usings, mainCode] = sliceIntoTwo(sourceCode, usingsLastLine);
        const linesStart = usingsLastLine + 1;

        const code = highlightLinesRange
            ? <CodeHighlight id="preview-source-code" {...this.props} linesStart={linesStart} highlightLinesRange={highlightLinesRange}>
                {mainCode}
            </CodeHighlight>
            : <CodePreview id="preview-source-code" {...this.props} linesStart={linesStart}>
                {mainCode}
            </CodePreview>;

        return <div className="demo-code">
            {usings && <Usings language={language}>{usings}</Usings>}
            {code}
        </div>;
    }
}

function mapStateToProps({ demos }: AppState): CodeProps {
    const { demo: dto } = demos;
    const wt = getCurrentWalkthrough(demos);
    return {
        language: demos.language,
        sourceCode: dto && dto.sourceCode,
        usingsLastLine: dto && dto.usingsLastLine,
        highlightLinesRange: wt && wt.lines
    };
}

export const Code = connect<CodeProps>(mapStateToProps)(CodeDisplay);