import * as React from "react";
import { Language } from "../../../features/common/commonModels";
import { CodePreview } from "../../helpers/CodePreview";

interface UsingsProps {
    language: Language;
}

export class Usings extends React.Component<UsingsProps, {}> {
    getTitle() {
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
        const { language, children } = this.props;
        return <div>
            <a className="folding collapsed" data-toggle="collapse" href="#includes" role="button" aria-expanded="false" aria-controls="includes">
                {this.getTitle()}
            </a>
            <div className="collapse" id="includes">
                <div>
                    <CodePreview id="preview-usings" language={language}>
                        {children}
                    </CodePreview>
                </div>
            </div>
        </div>;
    }
}