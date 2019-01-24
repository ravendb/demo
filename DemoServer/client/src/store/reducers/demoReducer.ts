import { modifyState } from "../state";
import { DemoAction } from "../actions/demoActions";
import { LocationChangeAction } from "connected-react-router";
import { matchDemoPath, matchDemoWithWalkthroughPath } from "../../utils/paths";
import { DemoState } from "../state/DemoState";
import { DemoEntry, WalkthroughEntry } from "../state/models";
import { Progress } from "../../utils/localStorage/Progress";
import { selectIsLastWalkthroughActive } from "../selectors/walkthroughs";
import { selectDemoVersionInfo } from "../selectors/demos";

const initialState: DemoState = {
    language: "csharp",
    categorySlug: null,
    demoSlug: null,
    wtSlug: null,
    demo: null,
    finishedLoadingDemo: false,
    finishedSettingPrerequisites: false,
    showResultsPanel: false,
    loadingRunResults: false,
    runResults: null,
    showShareMessage: false,
    userProgress: null,
    categories: [],
    loadingMainPage: false,
    conferenceMode: false
};

const getActiveWalkthroughs = (walkthroughs: WalkthroughEntry[], slug: string) => walkthroughs.map(w =>
    (w.slug === slug)
        ? { ...w, isActive: true }
        : { ...w, isActive: false }
);

const getAllInactiveWalkthroughs = (walkthroughs: WalkthroughEntry[]) => walkthroughs.map(w => ({ ...w, isActive: false }));

const updateWalkthroughAndProgress = (state: DemoState) => {
    const { demo, wtSlug } = state;

    if (!demo) {
        return;
    }

    if (demo.walkthroughs) {
        demo.walkthroughs = wtSlug
            ? getActiveWalkthroughs(demo.walkthroughs, wtSlug)
            : getAllInactiveWalkthroughs(demo.walkthroughs);
    }

    const isLastWalkthroughActive = selectIsLastWalkthroughActive(state);
    
    if (isLastWalkthroughActive) {
        const demoVersionInfo = selectDemoVersionInfo(state);
        Progress.save(demoVersionInfo);
    }
}

export function demoReducer(state: DemoState = initialState, action: DemoAction | LocationChangeAction): DemoState {
    switch (action.type) {
        case "DEMO_GET_VERSIONS_REQUEST":
            return modifyState(state, s => {
                s.userProgress = Progress.get();
                s.loadingMainPage = true;
            });

        case "DEMO_GET_VERSIONS_SUCCESS":
            return modifyState(state, s => {
                const { demoVersions, conferenceMode } = action;
                s.categories = demoVersions;
                s.conferenceMode = conferenceMode;
                s.userProgress = Progress.get();
                s.loadingMainPage = false;
            });

        case "DEMO_GET_METADATA_REQUEST":
            return modifyState(state, s => s.finishedLoadingDemo = false);

        case "DEMO_GET_METADATA_SUCCESS":
            return modifyState(state, s => {
                s.finishedLoadingDemo = true;
                s.demo = action.result as DemoEntry;

                updateWalkthroughAndProgress(s);
            });

        case "DEMO_GET_METADATA_FAILURE":
            return modifyState(state, s => {
                s.finishedLoadingDemo = true;
            });

        case "@@router/LOCATION_CHANGE":
            return modifyState(state, s => {
                const pathParams = matchDemoWithWalkthroughPath(action) || matchDemoPath(action);

                if (pathParams) {
                    s.categorySlug = pathParams.category;
                    s.demoSlug = pathParams.demo;
                    s.wtSlug = pathParams.wtSlug;
                }

                updateWalkthroughAndProgress(s);
            });

        case "DEMO_SET_PREREQUISITES_REQUEST":
            return modifyState(state, s => s.finishedSettingPrerequisites = false);

        case "DEMO_SET_PREREQUISITES_FAILURE":
        case "DEMO_SET_PREREQUISITES_SUCCESS":
            return modifyState(state, s => s.finishedSettingPrerequisites = true);

        case "DEMO_RUN_REQUEST":
            return modifyState(state, s => {
                s.showResultsPanel = true;
                s.loadingRunResults = true;
            });

        case "DEMO_RUN_SUCCESS":
            return modifyState(state, s => {
                s.loadingRunResults = false;
                s.runResults = action.results;
                s.userProgress = Progress.get();
            });

        case "DEMO_RUN_FAILURE":
            return modifyState(state, s => {
                s.loadingRunResults = false;
            });

        case "DEMO_HIDE_RESULTS":
            return modifyState(state, s => {
                s.showResultsPanel = false
            });

        case "DEMO_TOGGLE_SHARE_MESSAGE":
            return modifyState(state, s => {
                s.showShareMessage = action.show;
            });
    }

    return state;
}