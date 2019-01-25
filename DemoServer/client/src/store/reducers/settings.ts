import { SettingsAction } from "../actions/settings";
import { modifyState } from "../state";
import { SettingsState } from "../state/settings";
import { Progress } from "../../utils/localStorage/Progress";

const initialState: SettingsState = {
    showResetDatabaseDialog: false,
    resettingDatabase: false,
    showResetProgressDialog: false
};

export function settingsReducer(state: SettingsState = initialState, action: SettingsAction): SettingsState {
    switch (action.type) {
        case "SETTINGS_RESET_DB_DIALOG_SHOW":
            return modifyState(state, s => { s.showResetDatabaseDialog = true; });

        case "SETTINGS_RESET_DB_DIALOG_CLOSE":
            return modifyState(state, s => { s.showResetDatabaseDialog = false; });

        case "SETTINGS_RESET_DB_REQUEST":
            return modifyState(state, s => { s.resettingDatabase = true; });

        case "SETTINGS_RESET_DB_SUCCESS":
        case "SETTINGS_RESET_DB_FAILURE":
            return modifyState(state, s => {
                s.resettingDatabase = false;
                s.showResetDatabaseDialog = false;
            });

        case "SETTINGS_RESET_PROGRESS_DIALOG_SHOW":
            return modifyState(state, s => { s.showResetProgressDialog = true; });

        case "SETTINGS_RESET_PROGRESS_DIALOG_CLOSE":
            return modifyState(state, s => { s.showResetProgressDialog = false; });

        case "SETTINGS_CLEAR_PROGRESS":
            Progress.reset();
            return modifyState(state, s => { s.showResetProgressDialog = false; });
    }

    return state;
}
