import { DemoState } from "../state/DemoState";
import { createDemoWithWalkthroughPath } from "../../utils/paths";
import { CategorySlug, DemoSlug } from "../../models/slugs";
import { DemoWalkthroughDto } from "../../models/dtos/demo";

export interface DemoPathSlugs {
    category: CategorySlug;
    demo: DemoSlug;
}

export const getDemoUrlForType = (category: CategorySlug, demo: DemoSlug): string =>
    createDemoWithWalkthroughPath({
        category,
        demo
    });

export function getWalkthroughUrl(slugs: DemoPathSlugs, wt: DemoWalkthroughDto): string {
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

export const getDemoSlugs = (state: DemoState): DemoPathSlugs => {
    const { demoSlug, categorySlug } = state;

    return {
        category: categorySlug,
        demo: demoSlug
    };
}