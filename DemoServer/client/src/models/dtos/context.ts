import { DemoSlug, CategorySlug } from "../slugs";

export interface DemoHeaderDto {
    slug: DemoSlug;
    title: string;
    hash: string;
}

export interface CategoryHeaderDto {
    slug: CategorySlug;
    title: string;
    demos: DemoHeaderDto[];    
}

export interface DemoContextDto {
    categories: CategoryHeaderDto[];
    conferenceMode: boolean;
}
