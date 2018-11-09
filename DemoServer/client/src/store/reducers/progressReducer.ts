import { modifyState } from "../state";
import { ProgressState } from "../state/ProgressState";
import { ProgressAction } from "../actions/progressActions";
import { Progress } from "../../utils/localStorage/Progress";

const initialState: ProgressState = {
    userProgress: null
};

export function progressReducer(state: ProgressState = initialState, action: ProgressAction): ProgressState {
    switch (action.type) {
        case "PROGRESS_GET":
            return modifyState(state, s => {
                s.userProgress = Progress.get()
            });

        case "PROGRESS_SAVE":
            Progress.save(action.category, action.demo);
            return modifyState(state, s => {
                s.userProgress = Progress.get()
            });
    }
    return state;
}