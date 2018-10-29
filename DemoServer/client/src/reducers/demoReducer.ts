import { DemoState, modifyState } from "../store/state";
import { DemoAction } from "../actions/demoActions";

export function demoReducer(state: DemoState = { demo: null, loadingDemo: false }, action: DemoAction): DemoState {
    switch (action.type) {
        case "DEMO_GET_METADATA_REQUEST":
            return modifyState(state, s => s.loadingDemo = true);

        case "DEMO_GET_METADATA_SUCCESS":
            return modifyState(state, s => {
                s.loadingDemo = false;
                s.demo = action.result
            });

        case "DEMO_GET_METADATA_FAILURE":
            return state;
    }

    return state;
}