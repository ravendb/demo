import * as React from "react";
import { CodePreview } from "../../../helpers/Preview";
import { Language } from "../../../../features/common/commonModels";

interface UsingsProps {
    title: string;
    language: Language;
}

class Usings extends React.Component<UsingsProps, {}> {
    render() {
        const { title, language, children } = this.props;
        return <div>
            <a className="folding collapsed" data-toggle="collapse" href="#includes" role="button" aria-expanded="false" aria-controls="includes">
                {title}
            </a>
            <div className="collapse" id="includes">
                <div>
                    <CodePreview language={language}>
                        {children}
                    </CodePreview>
                </div>
            </div>
        </div>;
    }
}

interface SourceCodeProps {
    linesStart: number;
    language: Language;
}

class SourceCode extends React.Component<SourceCodeProps, {}> {
    render() {
        const { children } = this.props;
        return <CodePreview {...this.props}>
            {children}
        </CodePreview>;
    }
}

interface CodeProps {
    language: Language;
    usings: string;
    sourceCode: string;
}

export class Code extends React.Component<CodeProps, {}> {
    constructor(props) {
        super(props);
    }

    usingsTitle() {
        const { language } = this.props;
        switch (language) {
            case "csharp":
                return "usings";
            case "java":
            case "python":
                return "imports";
            default:
                return "usings";
        }
    }

    render() {
        const { language, usings, sourceCode } = this.props;
        const usingsLinesCnt = usings ? usings.split("\n").length : 0;
        return <div className="demo-code">
            <Usings language={language} title={this.usingsTitle()}>{usings}</Usings>
            <SourceCode language={language} linesStart={usingsLinesCnt + 1}>{sourceCode}</SourceCode>
        </div>;
    }
}