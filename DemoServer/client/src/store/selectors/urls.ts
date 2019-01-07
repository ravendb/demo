import { DemoState } from "../state/DemoState";
import { createDemoWithWalkthroughPath } from "../../utils/paths";
import { DemoWalkthroughDto } from "../../models/dtos";
import { DemoType } from "../../components/demos/demoTypes";
import { categoryList, Category } from "../../components/demos/categories";

export interface DemoSlugs {
    category: string;
    demo: string;
}

const getDemoForType = (demoType: DemoType, category: Category) => category.demos.find(x => x.type === demoType);

function getDemoSlugsForType(demoType: DemoType): DemoSlugs {
    for (let category of categoryList) {
        const demoMatch = getDemoForType(demoType, category);

        if (demoMatch) {
            return {
                category: category.slug,
                demo: demoMatch.slug
            };
        }
    }

    return null;
}

function getDemoUrl(slugs: DemoSlugs): string {
    const { category, demo } = slugs;

    return createDemoWithWalkthroughPath({
        category,
        demo,
    });
}

export function getDemoUrlForType(demoType: DemoType) {
    const demoSlugs = getDemoSlugsForType(demoType);
    return getDemoUrl(demoSlugs);
}

export function getWalkthroughUrl(slugs: DemoSlugs, wt: DemoWalkthroughDto): string {
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

export const getDemoSlugs = (state: DemoState): DemoSlugs => {
    const { demoSlug, categorySlug } = state;

    return {
        category: categorySlug,
        demo: demoSlug
    };
}