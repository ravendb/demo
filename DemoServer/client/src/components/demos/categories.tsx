import { DemoType } from "./demoTypes";

interface DemoInfo {
    slug: string;
    title: string;
    type: DemoType;
}

export interface Category {
    slug: string;
    title: string;
    demos: DemoInfo[];
}

const basicsCategory: Category = {
    slug: "basics",
    title: "Basics",
    demos: [
        {
            slug: "create-document",
            title: "Create Document",
            type: "DEMO_CreateDocument"
        },
        {
            slug: "edit-document",
            title: "Edit Document",
            type: "DEMO_EditDocument"
        }
    ]
};

const queriesCategory: Category = {
    slug: "queries",
    title: "Queries",
    demos: [
        {
            slug: "simple-query",
            title: "Simple Query",
            type: "DEMO_SimpleQuery"
        }
    ]
};

const advancedCategory: Category = {
    slug: "advanced",
    title: "Advanced",
    demos: [
        {
            slug: "create-database",
            title: "Create Database",
            type: "DEMO_CreateDatabase"
        }
    ]
};

export const categoryList: Category[] = [
    basicsCategory,
    queriesCategory,
    advancedCategory
];
