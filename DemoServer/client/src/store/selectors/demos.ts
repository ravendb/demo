import { createSelector } from "reselect";
import { DemoState } from "../state/demo";
import { getDemoSlugs } from "./urlGetters";
import { CategorySlug, DemoSlug } from "../../models/slugs";

export interface DemoVersionInfo {
    category: CategorySlug;
    demo: DemoSlug;
    demoHash: string;
}

const getDemoHash = (state: DemoState): string => {
    const { categorySlug, demoSlug, categoriesWithVersions } = state;

    const category = categoriesWithVersions.find(x => x.slug === categorySlug);
    const demo = category && category.demos && category.demos.find(x => x.slug === demoSlug);
    return demo && demo.hash;
};

const selectDemoHash = createSelector(
    [getDemoHash],
    (demoHash) => demoHash
);

export const selectDemoVersionInfo = createSelector(
    [getDemoSlugs, selectDemoHash],
    (demoSlugs, demoHash): DemoVersionInfo => {
        const { category, demo } = demoSlugs;

        return { category, demo, demoHash };
    }
);
