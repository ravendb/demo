import * as React from "react";
import { Demo101 } from "./basics/Demo101";
import { categoryList } from "./categories";
import { AppState } from "../../store/state";
import { connect } from "react-redux";

const DemoNotFound = () => {
    return <>
        <h2>Demo not found</h2>
        <p>We're sorry, this is not the demo you're looking for.</p>
    </>;
}

function getDemoType(categorySlug: string, demoSlug: string) {
    var category = categoryList.find(x => x.slug === categorySlug);
    if (!category || !category.demos) {
        return null;
    }

    var demo = category.demos.find(x => x.slug == demoSlug);
    return demo && demo.type;
}

interface DemoFactoryProps {
    categorySlug: string;
    demoSlug: string;
}

function DemoFactoryComponent(props: DemoFactoryProps) {
    const { categorySlug, demoSlug } = props;
    const demoType = getDemoType(categorySlug, demoSlug);

    switch (demoType) {
        case "DEMO_101":
            return <Demo101 />;
    }

    return <DemoNotFound />;
}

function mapStateToProps({ demos }: AppState): DemoFactoryProps {
    const { categorySlug, demoSlug } = demos;
    return {
        categorySlug,
        demoSlug
    };
}

export const DemoFactory = connect<DemoFactoryProps>(mapStateToProps)(DemoFactoryComponent);