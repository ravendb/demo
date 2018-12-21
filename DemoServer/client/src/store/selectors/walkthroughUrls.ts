import { createSelector } from "reselect"
import { DemoState } from "../state/DemoState";
import { DemoWalkthroughDto } from "../../models/dtos";
import { createDemoWithWalkthroughPath, createDemoWithoutWalkthroughPath } from "../../utils/paths";
import { selectWalkthroughs, selectFirstWalkthrough, selectPreviousWalkthrough, selectNextWalkthrough } from "./walkthroughs";

function getWalkthroughUrl(slugs: DemoSlugs, wt: DemoWalkthroughDto): string {
    if (!wt) {
        return null;
    }

    const { category, demo } = slugs;
    return createDemoWithWalkthroughPath({
        category,
        demo,
        wtSlug: wt.slug
    });
}

interface DemoSlugs {
    category: string;
    demo: string;
}

const getDemoSlugs = (state: DemoState): DemoSlugs => {
    const { demoSlug, categorySlug } = state;

    return {
        category: categorySlug,
        demo: demoSlug
    };
}

export const selectFirstWalkthroughUrl = createSelector(
    [getDemoSlugs, selectFirstWalkthrough],
    (demoSlugs, firstWt) => firstWt ? getWalkthroughUrl(demoSlugs, firstWt) : null
);

export const selectPreviousWalkthroughUrl = createSelector(
    [getDemoSlugs, selectPreviousWalkthrough],
    (demoSlugs, previousWt) => previousWt ? getWalkthroughUrl(demoSlugs, previousWt) : null
);

export const selectWalkthroughUrls = createSelector(
    [getDemoSlugs, selectWalkthroughs],
    (demoSlugs, wts): string[] => wts.map(x => getWalkthroughUrl(demoSlugs, x))
);

export const selectNextWalkthroughUrl = createSelector(
    [getDemoSlugs, selectNextWalkthrough],
    (demoSlugs, nextWt) => nextWt ? getWalkthroughUrl(demoSlugs, nextWt) : null
);

export const selectUrlWithoutWalkthrough = createSelector(
    [getDemoSlugs],
    (demoSlugs) => {
        const { category, demo } = demoSlugs;
        return createDemoWithoutWalkthroughPath({ category, demo });
    }
);
