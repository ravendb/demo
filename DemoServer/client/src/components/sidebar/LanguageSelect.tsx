import * as React from "react";
import { Language, languageToDisplay } from "../../models/commonModels";

interface LanguageSelectProps {
    selected: Language;
}

export class LanguageSelect extends React.Component<LanguageSelectProps, {}> {
    button(language: Language) {
        const { selected } = this.props;
        const active = language === selected;
        const activeClassName = active ? " active" : "";
        const className = "btn" + activeClassName;
        return <button className={className}>{languageToDisplay(language)}</button>
    }

    render() {
        return <div className="language-select">
            {this.button("csharp")}
            {this.button("java")}
            {this.button("python")}
        </div>;
    }
}