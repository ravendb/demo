import { DemoState } from "../state/demo";
import { createDemoWithWalkthroughPath } from "../../utils/paths";
import { CategorySlug, DemoSlug } from "../../models/slugs";
import { DemoWalkthroughDto } from "../../models/dtos/demo";
import { Language } from "../../models/common";

export interface DemoPathSlugs {
    category: CategorySlug;
    demo: DemoSlug;
    language: Language;
}

export function getWalkthroughUrl(slugs: DemoPathSlugs, wt: DemoWalkthroughDto): string {
    if (!wt) {
        return null;
    }

    const { category, demo, language } = slugs;
    return createDemoWithWalkthroughPath({
        category,
        demo,
        wtSlug: wt.slug,
        language
    });
}

export const getDemoSlugs = (state: DemoState): DemoPathSlugs => {
    const { demoSlug, categorySlug, language } = state;

    return {
        category: categorySlug,
        demo: demoSlug,
        language
    };
};
