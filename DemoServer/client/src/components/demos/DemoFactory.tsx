import * as React from "react";
import { CreateDatabaseDemo } from "./advanced/CreateDatabaseDemo";
import { CreateDocumentDemo } from "./basics/CreateDocumentDemo";
import { EditDocumentDemo } from "./basics/EditDocumentDemo";
import { SimpleQueryDemo } from "./queries/SimpleQueryDemo";
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
        // Category Basics         
        case "DEMO_CreateDocument":
            return <CreateDocumentDemo />;
        case "DEMO_EditDocument":
            return <EditDocumentDemo />;
            
        // Category Queries    
        case "DEMO_SimpleQuery":
            return <SimpleQueryDemo />;
        
        // Category Advanced
        case "DEMO_CreateDatabase":
            return <CreateDatabaseDemo />;   
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