import { DemoState, getCurrentWalkthroughIndex } from "../state/DemoState";
import { DemoWalkthroughDto } from "../../models/dtos";
import { createDemoWithWalkthroughPath, createDemoWithoutWalkthroughPath } from "../../utils/paths";

function getWalkthroughsCount(state: DemoState) {
    const wts = state.demo && state.demo.walkthroughs;
    return wts ? wts.length : 0;
}

function getWalkthroughUrl(state: DemoState, wt: DemoWalkthroughDto): string {
    if (!wt) {
        return null;
    }

    const { categorySlug, demoSlug } = state;
    return createDemoWithWalkthroughPath({
        category: categorySlug,
        demo: demoSlug,
        wtSlug: wt.slug
    });
}

export function getFirstWalkthroughUrl(state: DemoState): string {
    const { demo } = state;
    const firstWt = demo && demo.walkthroughs
        && demo.walkthroughs.length > 0
        && demo.walkthroughs[0];
    return getWalkthroughUrl(state, firstWt);
}

function getPreviousWalkthrough(state: DemoState): DemoWalkthroughDto {
    const i = getCurrentWalkthroughIndex(state);
    return i > 0 ? state.demo.walkthroughs[i - 1] : null;
}

export function getPreviousWalkthroughUrl(state: DemoState): string {
    const previousWt = getPreviousWalkthrough(state);
    return getWalkthroughUrl(state, previousWt);
}

function getNextWalkthrough(state: DemoState): DemoWalkthroughDto {
    const i = getCurrentWalkthroughIndex(state);
    const wtCount = getWalkthroughsCount(state);
    return i < wtCount - 1 ? state.demo.walkthroughs[i + 1] : null;
}

export function getNextWalkthroughUrl(state: DemoState): string {
    const nextWt = getNextWalkthrough(state);
    return getWalkthroughUrl(state, nextWt);
}

export function getUrlWithoutWalkthrough(state: DemoState): string {
    const { categorySlug, demoSlug } = state;
    return createDemoWithoutWalkthroughPath({
        category: categorySlug,
        demo: demoSlug
    });
}