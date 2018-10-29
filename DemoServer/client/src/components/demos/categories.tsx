import { DemoType } from "./demoNames";

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

export const categoryList: Category[] = [
    {
        slug: "basics",
        title: "Basics",
        demos: [
            {
                slug: "demo101",
                title: "The First Demo",
                type: "DEMO_101"
            }
        ]
    }
]