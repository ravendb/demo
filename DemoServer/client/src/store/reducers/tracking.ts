import { modifyState } from "../state";
import { TrackingAction } from "../actions/tracking";
import { TrackingState } from "../state/tracking";
import { NavigationAction } from "../actions/navigation";

const initialState: TrackingState = {
    enabled: false,
    googleTagManagerContainerId: null,
    showConstentMonit: false
};

export function trackingReducer(state: TrackingState = initialState,
                                action: TrackingAction | NavigationAction): TrackingState {
    switch (action.type) {

        case "TRACKING_SAVE_GTM_CONTAINER_ID":
            return modifyState(state, s => {
                s.googleTagManagerContainerId = action.googleTagManagerContainerId;
            });

        case "TRACKING_SHOW_MONIT":
            return modifyState(state, s => {
                s.showConstentMonit = true;
            });

        case "TRACKING_WITHDRAW_CONSENT":
            return modifyState(state, s => {
                s.enabled = false;
                s.showConstentMonit = false;
            });

        case "TRACKING_ENABLE":
            return modifyState(state, s => {
                s.enabled = true;
                s.showConstentMonit = false;
            });

    }

    return state;
}
