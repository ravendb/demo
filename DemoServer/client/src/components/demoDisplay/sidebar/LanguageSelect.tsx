import * as React from "react";
import * as classNames from "classnames";
import { Language, languageToDisplay } from "../../../models/common";

interface LanguageSelectProps {
    selected: Language;
}

export class LanguageSelect extends React.Component<LanguageSelectProps, {}> {
    button(language: Language) {
        const { selected } = this.props;
        const active = language === selected;

        const className = classNames("btn", {
            "active": active
        });

        return <button className={className}>{languageToDisplay(language)}</button>
    }

    render() {
        return null;
        // return <div className="language-select">
        //     {this.button("csharp")}
        //     {this.button("java")}
        //     {this.button("python")}
        // </div>;
    }
}