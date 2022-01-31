import { createSelector } from "reselect";
import { DemoState } from "../state/demo";
import { getDemoSlugs } from "./urlGetters";
import { CategorySlug, DemoSlug } from "../../models/slugs";
import { Language } from "../../models/common";
import { CategoryForLanguage } from "../../models/dtos/context";

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

function doCategoriesContainDemo(
    categories: CategoryForLanguage[],
    categorySlug: CategorySlug,
    demoSlug: DemoSlug): boolean {

    const category = categories
        && categories.find(x => x.slug === categorySlug);

    if (!category) {
        return false;
    }

    const demo = category.demos && category.demos.find(x => x.slug === demoSlug);

    return !!demo;
}

function getLanguagesForDemo(state: DemoState): Language[] {
    const { categoriesForLanguages, categorySlug, demoSlug } = state;

    const languages: Language[] = [];

    for (const categoriesForLanguage of categoriesForLanguages) {

        const categories = categoriesForLanguage.categories;

        const containDemo = doCategoriesContainDemo(categories, categorySlug, demoSlug);

        if (containDemo) {
            languages.push(categoriesForLanguage.language);
        }
    }

    return languages;
}

export const selectLanguagesForDemo = createSelector(
    [getLanguagesForDemo],
    (languages) => languages
);

export function getDemoImageSrc(category: CategorySlug, demo: DemoSlug): string {
    return `/img/demo-icons/${category}/${demo}.svg`;
}
