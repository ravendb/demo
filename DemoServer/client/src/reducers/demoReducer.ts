import { DemoState, modifyState } from "../store/state";
import { DemoAction } from "../actions/demoActions";
import { LocationChangeAction } from "connected-react-router";
import { matchDemoPath } from "../utils/paths";

const initialState: DemoState = {
    language: "csharp",
    categorySlug: "",
    demoSlug: "",
    demo: null,
    loadingDemo: false,
    loadingRunResults: false,
    parameters: [],
    runResults: null
};

export function demoReducer(state: DemoState = initialState, action: DemoAction | LocationChangeAction): DemoState {
    switch (action.type) {
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
                const params = matchDemoPath(action);
                if (params) {
                    s.categorySlug = params.category;
                    s.demoSlug = params.demo;
                }
            });

        case "DEMO_RUN_REQUEST":
            return modifyState(state, s => s.loadingRunResults = true);

        case "DEMO_RUN_SUCCESS":
            return modifyState(state, s => {
                s.loadingRunResults = false;
                s.runResults = action.results;
            });

        case "DEMO_RUN_FAILURE":
            return modifyState(state, s => {
                s.loadingRunResults = false;
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