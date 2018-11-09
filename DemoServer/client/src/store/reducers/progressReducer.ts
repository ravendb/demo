import { modifyState } from "../state";
import { ProgressState } from "../state/ProgressState";
import { ProgressAction } from "../actions/progressActions";
import { Progress } from "../../utils/localStorage/Progress";

const initialState: ProgressState = {
    userProgress: null,
    demoVersions: []
};

const refreshUserProgress = (state: ProgressState) => {
    return modifyState(state, s => {
        s.userProgress = Progress.get();
    });
}

function getDemoHash(category: string, demo: string, { demoVersions }: ProgressState): string {
    const match = demoVersions.find(x => x.category === category && x.demo === demo);
    return match && match.hash;
}

export function progressReducer(state: ProgressState = initialState, action: ProgressAction): ProgressState {
    switch (action.type) {
        case "PROGRESS_GET_VERSIONS_REQUEST":
            return refreshUserProgress(state);

        case "PROGRESS_GET_VERSIONS_FAILURE":
            return state;

        case "PROGRESS_GET_VERSIONS_SUCCESS":
            Progress.updateDemoVersions(action.results);
            return modifyState(state, s => {
                s.demoVersions = action.results;
                s.userProgress = Progress.get();
            });

        case "PROGRESS_SAVE":
            const { category, demo } = action;
            const demoHash = getDemoHash(category, demo, state);
            Progress.save(category, demo, demoHash);
            return refreshUserProgress(state);
    }
    return state;
}