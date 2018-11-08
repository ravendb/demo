import { modifyState } from "../state";
import { DemoAction } from "../actions/demoActions";
import { LocationChangeAction } from "connected-react-router";
import { matchDemoPath, matchDemoWithWalkthroughPath } from "../../utils/paths";
import { DemoState } from "../state/DemoState";
import { DemoStorage } from "../../utils/DemoStorage";

const initialState: DemoState = {
    language: "csharp",
    userProgress: null,
    categorySlug: "",
    demoSlug: "",
    demo: null,
    loadingDemo: false,
    settingPrerequisites: false,
    showResultsPanel: false,
    loadingRunResults: false,
    parameters: [],
    runResults: null
};

export function demoReducer(state: DemoState = initialState, action: DemoAction | LocationChangeAction): DemoState {
    switch (action.type) {
        case "PROGRESS_GET":
            return modifyState(state, s => {
                s.userProgress = DemoStorage.getUserProgress()
            });

        case "DEMO_GET_METADATA_REQUEST":
            return modifyState(state, s => s.loadingDemo = true);

        case "DEMO_GET_METADATA_SUCCESS":
            return modifyState(state, s => {
                s.loadingDemo = false;
                s.demo = action.result
            });

        case "DEMO_GET_METADATA_FAILURE":
            return modifyState(state, s => {
                s.loadingDemo = false;
            });

        case "@@router/LOCATION_CHANGE":
            return modifyState(state, s => {
                const params = matchDemoWithWalkthroughPath(action) || matchDemoPath(action);
                if (params) {
                    s.categorySlug = params.category;
                    s.demoSlug = params.demo;
                    s.currentWalkthroughSlug = params.wtSlug;
                }
            });

        case "DEMO_SET_PREREQUISITES_REQUEST":
            return modifyState(state, s => s.settingPrerequisites = true);

        case "DEMO_SET_PREREQUISITES_FAILURE":
        case "DEMO_SET_PREREQUISITES_SUCCESS":
            return modifyState(state, s => s.settingPrerequisites = false);

        case "DEMO_RUN_REQUEST":
            return modifyState(state, s => {
                s.showResultsPanel = true;
                s.loadingRunResults = true;
            });

        case "DEMO_RUN_SUCCESS":
            return modifyState(state, s => {
                s.loadingRunResults = false;
                s.runResults = action.results;
            });

        case "DEMO_RUN_FAILURE":
            return modifyState(state, s => {
                s.loadingRunResults = false;
            });

        case "DEMO_HIDE_RESULTS":
            return modifyState(state, s => {
                s.showResultsPanel = false
            });

        case "DEMO_PARAMS_INIT":
            return modifyState(state, s => {
                s.parameters = action.parameters;
            });

        case "DEMO_PARAMS_CHANGE":
            return modifyState(state, s => {
                s.parameters = s.parameters.map(x => x.name === action.name
                    ? { ...x, value: action.value }
                    : x);
            });
    }

    return state;
}