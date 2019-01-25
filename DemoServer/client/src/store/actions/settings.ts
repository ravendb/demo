import * as actionTypes from "./actionTypes";

interface ShowResetDatabasePopup {
    type: actionTypes.SETTINGS_RESET_DB_POPUP_SHOW;
}

interface CloseResetDatabasePopup {
    type: actionTypes.SETTINGS_RESET_DB_POPUP_CLOSE;
}

interface ShowResetProgressPopup {
    type: actionTypes.SETTINGS_RESET_PROGRESS_POPUP_SHOW;
}

interface CloseResetProgressPopup {
    type: actionTypes.SETTINGS_RESET_PROGRESS_POPUP_CLOSE;
}

export type SettingsAction = ShowResetDatabasePopup | CloseResetDatabasePopup
    | ShowResetProgressPopup | CloseResetProgressPopup;

export const showResetDatabasePopup = (): ShowResetDatabasePopup => ({
    type: "SETTINGS_RESET_DB_POPUP_SHOW"
});

export const closeResetDatabasePopup = (): CloseResetDatabasePopup => ({
    type: "SETTINGS_RESET_DB_POPUP_CLOSE"
});

export const showResetProgressPopup = (): ShowResetProgressPopup => ({
    type: "SETTINGS_RESET_PROGRESS_POPUP_SHOW"
});

export const closeResetProgressPopup = (): CloseResetProgressPopup => ({
    type: "SETTINGS_RESET_PROGRESS_POPUP_CLOSE"
});
