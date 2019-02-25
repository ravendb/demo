import { DemoSlug, CategorySlug } from "../slugs";
import { Language } from "../common";

export interface DemoForLanguage {
    slug: DemoSlug;
    title: string;
}

export interface CategoryForLanguage {
    slug: CategorySlug;
    demos: DemoForLanguage[];    
}

export interface CategoriesForLanguage {
    language: Language;
    categories: CategoryForLanguage[];
}

export interface DemoWithVersion {
    slug: DemoSlug;
    title: string;
    hash: string;
}

export interface CategoryWithDemoVersions {
    slug: CategorySlug;
    title: string;
    demos: DemoWithVersion[];
}

export interface DemoContextDto {
    categoriesForLanguages: CategoriesForLanguage[];
    categoriesWithVersions: CategoryWithDemoVersions[];
    conferenceMode: boolean;
}
