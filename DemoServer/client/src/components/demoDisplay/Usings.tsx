import * as React from "react";
import { Language } from "../../models/commonModels";
import { CodePreview } from "../helpers/CodePreview";
import * as bsn from "bootstrap.native/dist/bootstrap-native-v4";

interface UsingsProps {
    language: Language;
}

export class Usings extends React.Component<UsingsProps, {}> {
    collapseButton: HTMLElement;

    componentDidMount() {
        this.collapseButton = document.getElementById("toggle-collapse-usings");
        bsn.Collapse(this.collapseButton);
    }

    componentWillUnmount() {
        bsn.Collapse(this.collapseButton, "dispose");
    }

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
            <a id="toggle-collapse-usings" className="folding collapsed" role="button" data-toggle="collapse" data-target="#includes" aria-expanded="false" aria-controls="includes">
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