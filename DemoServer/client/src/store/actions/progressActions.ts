import * as actionTypes from "./actionTypes";

export interface GetProgress {
    type: actionTypes.PROGRESS_GET;
}

export interface SaveProgress {
    type: actionTypes.PROGRESS_SAVE;
    category: string;
    demo: string;
}

export type ProgressAction = GetProgress | SaveProgress;

export function getProgress(): GetProgress {
    return {
        type: "PROGRESS_GET"
    };
}

export function updateProgress(category: string, demo: string): SaveProgress {
    return {
        type: "PROGRESS_SAVE",
        category,
        demo
    };
}