import { createSelector } from "reselect";
import { DemoState } from "../state/DemoState";
import { categoryList } from "../../components/demos/categories";
import { DemoType } from "../../components/demos/demoTypes";

const getDemoType = (state: DemoState): DemoType => {
    const { categorySlug, demoSlug } = state;

    var category = categoryList.find(x => x.slug === categorySlug);
    if (!category || !category.demos) {
        return null;
    }

    var demo = category.demos.find(x => x.slug == demoSlug);

    if (demo) {
        return demo.type;
    }

    return null;
}

export const selectDemoType = createSelector(
    [getDemoType],
    (demoType) => demoType
);