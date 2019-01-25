import { SettingsAction } from "../actions/settings";
import { modifyState } from "../state";
import { SettingsState } from "../state/settings";

const initialState: SettingsState = {
    showResetDatabaseConfirm: false,
    showResetProgressConfirm: false
};

export function settingsReducer(state: SettingsState = initialState, action: SettingsAction): SettingsState {
    switch (action.type) {
        case "SETTINGS_RESET_DB_POPUP_SHOW":
            return modifyState(state, s => { s.showResetDatabaseConfirm = true; });

        case "SETTINGS_RESET_DB_POPUP_CLOSE":
            return modifyState(state, s => { s.showResetDatabaseConfirm = false; });

        case "SETTINGS_RESET_PROGRESS_POPUP_SHOW":
            return modifyState(state, s => { s.showResetProgressConfirm = true; });

        case "SETTINGS_RESET_PROGRESS_POPUP_SHOW":
            return modifyState(state, s => { s.showResetProgressConfirm = false; });
    }

    return state;
}
