import { createSelector } from "reselect";
import { DemoState } from "../state/DemoState";
import { WalkthroughEntry } from "../state/models";

export const selectWalkthroughs = (state: DemoState): WalkthroughEntry[] => {
    const { demo } = state;
    return (demo && demo.walkthroughs)
        ? demo.walkthroughs
        : [];
};

export const selectIsAnyWalkthrough = createSelector(
    [selectWalkthroughs],
    (walkthroughs) => walkthroughs && !!walkthroughs.length
);

export const selectActiveWalkthrough = createSelector(
    [selectWalkthroughs],
    (walkthroughs) => walkthroughs.find(x => x.isActive)
);

export const selectActiveWalkthroughIndex = createSelector(
    [selectWalkthroughs],
    (walkthroughs) => walkthroughs.findIndex(x => x.isActive)
);

export const selectWalkthroughCount = createSelector(
    [selectWalkthroughs],
    (walkthroughs) => walkthroughs
        ? walkthroughs.length
        : 0
);

export const selectIsLastWalkthroughActive = createSelector(
    [selectWalkthroughs, selectIsAnyWalkthrough],
    (walkthroughs, anyWt) => anyWt
        ? walkthroughs[walkthroughs.length - 1].isActive
        : false
);

export const selectFirstWalkthrough = createSelector(
    [selectWalkthroughs, selectIsAnyWalkthrough],
    (walkthroughs, anyWt) => anyWt && walkthroughs[0]
);

export const selectPreviousWalkthrough = createSelector(
    [selectWalkthroughs, selectActiveWalkthroughIndex],
    (walkthroughs, activeIndex) => (activeIndex > 0) && walkthroughs[activeIndex - 1]
);

export const selectNextWalkthrough = createSelector(
    [selectWalkthroughs, selectActiveWalkthroughIndex, selectWalkthroughCount],
    (wts, activeIndex, count) => (activeIndex < count - 1) && wts[activeIndex + 1]
);