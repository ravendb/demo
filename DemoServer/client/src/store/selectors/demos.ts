import { createSelector } from "reselect";
import { DemoState } from "../state/DemoState";
import { getDemoSlugs, DemoPathSlugs } from "./urlGetters";

export interface DemoVersionInfo extends DemoPathSlugs {
    demoHash: string;
}

const getDemoHash = (state: DemoState): string => {
    const { categorySlug, demoSlug, categories } = state;

    const category = categories.find(x => x.slug === categorySlug);
    const demo = category && category.demos && category.demos.find(x => x.slug === demoSlug);
    return demo && demo.hash;
};

const selectDemoHash = createSelector(
    [getDemoHash],
    (demoHash) => demoHash
);

export const selectDemoVersionInfo = createSelector(
    [getDemoSlugs, selectDemoHash],
    (demoSlugs, demoHash): DemoVersionInfo => ({
        ...demoSlugs,
        demoHash
    })
);
