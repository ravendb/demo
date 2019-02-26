import * as React from "react";
import * as classNames from "classnames";
import { Language, languageToDisplay } from "../../../models/common";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { selectLanguagesForDemo } from "../../../store/selectors/demos";
import { DemoThunkDispatch } from "../../../store";
import { changeDemoLanguage } from "../../../store/actions/navigation";
import { defaultLanguage } from "../../../store/state/demo";
import { noop } from "../../../utils/functionUtils";

interface StateProps {
    selected: Language;
    available: Language[];
}

interface DispatchProps {
    changeLanguage: (language: Language) => void;
}

type Props = StateProps & DispatchProps;

class LanguageSelectComponent extends React.Component<Props, {}> {
    private _button(language: Language) {
        const { selected, changeLanguage } = this.props;
        const active = language === selected;

        const className = classNames("btn", {
            "active": active
        });

        const onClick = active
            ? noop
            : () => changeLanguage(language);

        return <button key={language} className={className} onClick={onClick}>
            {languageToDisplay(language)}
        </button>;
    }

    public render() {
        const { available } = this.props;

        if (!available || !available.length) {
            return null;
        }

        if (available.length === 1 && available[0] === defaultLanguage) {
            return null;
        }

        return <div className="language-select">
            {available.map(lang => this._button(lang))}
        </div>;
    }
}

function mapStateToProps({ demos }: AppState): StateProps {
    const { language } = demos;
    const availableLanguages = selectLanguagesForDemo(demos);

    return {
        selected: language,
        available: availableLanguages
    };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): DispatchProps {
    return {
        changeLanguage: (language: Language) => dispatch(changeDemoLanguage(language))
    };
}

export const LanguageSelect = connect<StateProps, DispatchProps>(
    mapStateToProps,
    mapDispatchToProps
)(LanguageSelectComponent);
