import * as actionTypes from "./actionTypes";
import { DemoThunkAction } from ".";
import { DemoThunkDispatch } from "..";
import { DemoService } from "../../utils/api/Services";
import { apiError } from "./error";
import { refreshProgress } from "./demo";

const service = new DemoService();

interface ShowResetDatabaseDialog {
    type: actionTypes.SETTINGS_RESET_DB_DIALOG_SHOW;
}

interface CloseResetDatabaseDialog {
    type: actionTypes.SETTINGS_RESET_DB_DIALOG_CLOSE;
}

interface ResetDatabaseRequest {
    type: actionTypes.SETTINGS_RESET_DB_REQUEST;
}

interface ResetDatabaseSuccess {
    type: actionTypes.SETTINGS_RESET_DB_SUCCESS;
}

interface ResetDatabaseFailure {
    type: actionTypes.SETTINGS_RESET_DB_FAILURE;
}

interface ShowResetProgressDialog {
    type: actionTypes.SETTINGS_RESET_PROGRESS_DIALOG_SHOW;
}

interface CloseResetProgressDialog {
    type: actionTypes.SETTINGS_RESET_PROGRESS_DIALOG_CLOSE;
}

interface ClearProgress {
    type: actionTypes.SETTINGS_CLEAR_PROGRESS;
}

export type SettingsAction = ShowResetDatabaseDialog | CloseResetDatabaseDialog
    | ResetDatabaseRequest | ResetDatabaseSuccess | ResetDatabaseFailure
    | ShowResetProgressDialog | CloseResetProgressDialog
    | ClearProgress;

export const showResetDatabaseDialog = (): ShowResetDatabaseDialog => ({
    type: "SETTINGS_RESET_DB_DIALOG_SHOW"
});

export const closeResetDatabaseDialog = (): CloseResetDatabaseDialog => ({
    type: "SETTINGS_RESET_DB_DIALOG_CLOSE"
});

const resetDatabaseRequest = (): ResetDatabaseRequest => ({
    type: "SETTINGS_RESET_DB_REQUEST"
});

const resetDatabaseSuccess = (): ResetDatabaseSuccess => ({
    type: "SETTINGS_RESET_DB_SUCCESS"
});

const resetDatabaseFailure = (): ResetDatabaseFailure => ({
    type: "SETTINGS_RESET_DB_FAILURE"
});

export function resetDatabase(): DemoThunkAction {
    return async (dispatch: DemoThunkDispatch) => {
        dispatch(resetDatabaseRequest());

        try {
            await service.resetDatabase();
            dispatch(resetDatabaseSuccess());
        } catch (error) {
            dispatch(resetDatabaseFailure());
            dispatch(apiError(error));
        }
    };
}

export const showResetProgressDialog = (): ShowResetProgressDialog => ({
    type: "SETTINGS_RESET_PROGRESS_DIALOG_SHOW"
});

export const closeResetProgressDialog = (): CloseResetProgressDialog => ({
    type: "SETTINGS_RESET_PROGRESS_DIALOG_CLOSE"
});

const clearProgress = (): ClearProgress => ({
    type: "SETTINGS_CLEAR_PROGRESS"
});

export function resetProgress(): DemoThunkAction {
    return (dispatch: DemoThunkDispatch) => {
        dispatch(clearProgress());
        dispatch(refreshProgress());
    };
}
