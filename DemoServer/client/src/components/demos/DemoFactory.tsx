import * as React from "react";
import { Demo101 } from "./basics/Demo101";
import { DemoProps, DemoNotFound } from "./Demo";
import { categoryList } from "./categories";

function getDemoType(categorySlug: string, demoSlug: string) {
    var category = categoryList.find(x => x.slug === categorySlug);
    if (!category || !category.demos) {
        return null;
    }

    var demo = category.demos.find(x => x.slug == demoSlug);
    return demo && demo.type;
}

export interface DemoFactoryProps {
    categorySlug: string;
    demoSlug: string;
}

export function DemoFactory(props: DemoFactoryProps) {
    const { categorySlug, demoSlug } = props;
    const demoType = getDemoType(categorySlug, demoSlug);

    switch (demoType) {
        case "DEMO_101":
            return <Demo101 {...props} />;
    }

    return <DemoNotFound {...props} loadMetadata={() => {}} />;
}