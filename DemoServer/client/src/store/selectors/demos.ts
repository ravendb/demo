import { createSelector } from "reselect";
import { DemoState } from "../state/DemoState";
import { categoryList } from "../../components/demos/categories";
import { DemoType } from "../../components/demos/demoTypes";
import { getDemoSlugs, DemoSlugs } from "./urlGetters";

export interface DemoVersionInfo extends DemoSlugs {
    demoHash: string;
}

export const getDemoType = (categorySlug: string, demoSlug: string): DemoType => {
    var category = categoryList.find(x => x.slug === categorySlug);
    if (!category || !category.demos) {
        return null;
    }

    var demo = category.demos.find(x => x.slug == demoSlug);

    if (demo) {
        return demo.type;
    }

    return null;
};

export const getDemoTypeFromState = (state: DemoState): DemoType => {
    const { categorySlug, demoSlug } = state;
    return getDemoType(categorySlug, demoSlug);
};

const getDemoHash = (state: DemoState): string => {
    const { categorySlug, demoSlug, demoVersions } = state;

    const match = demoVersions.find(x => x.category === categorySlug && x.demo === demoSlug);
    return match && match.hash;
};

const selectDemoHash = createSelector(
    [getDemoHash],
    (demoHash) => demoHash
);

export const selectDemoType = createSelector(
    [getDemoTypeFromState],
    (demoType) => demoType
);

export const selectDemoVersionInfo = createSelector(
    [getDemoSlugs, selectDemoHash],
    (demoSlugs, demoHash): DemoVersionInfo => ({
        ...demoSlugs,
        demoHash
    })
);