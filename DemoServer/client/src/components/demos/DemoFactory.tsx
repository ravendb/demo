import * as React from "react";
import { CreateDatabaseDemo } from "./advanced/CreateDatabaseDemo";
import { TheDocumentStoreDemo } from "./basics/TheDocumentStoreDemo";
import { TheSessionDemo } from "./basics/TheSessionDemo";
import { CreateDocumentDemo } from "./basics/CreateDocumentDemo";
import { EditDocumentDemo } from "./basics/EditDocumentDemo";
import { DeleteDocumentDemo } from "./basics/DeleteDocumentDemo";
import { StoreAttachmentDemo } from "./attachments/StoreAttachmentDemo";
import { EnableRevisionsDemo } from "./revisions/EnableRevisionsDemo";
import { FullCollectionQueryDemo } from "./queries/FullCollectionQueryDemo";
import { QueryByDocumentIdDemo } from "./queries/QueryByDocumentIdDemo";
import { FilteringQueryResultsDemo } from "./queries/FilteringQueryResultsDemo";
import { ProjectingIndividualFieldsDemo } from "./queries/ProjectingIndividualFieldsDemo";
import { ProjectingUsingFunctionsDemo } from "./queries/ProjectingUsingFunctionsDemo";
import { StaticIndexesOverviewDemo } from "./staticIndexes/StaticIndexesOverviewDemo";
import { MapIndexDemo } from "./staticIndexes/MapIndexDemo";
import { FullTextSearchWithStaticIndexDemo } from "./textSearch/FullTextSearchWithStaticIndexDemo";
import { QueryOverviewDemo } from "./queries/QueryOverviewDemo";
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
        // Category: Basics
        case "DEMO_TheDocumentStore":
            return <TheDocumentStoreDemo />;
        case "DEMO_TheSession":
            return <TheSessionDemo />;
        case "DEMO_CreateDocument":
            return <CreateDocumentDemo />;
        case "DEMO_EditDocument":
            return <EditDocumentDemo />;
        case "DEMO_DeleteDocument":
            return <DeleteDocumentDemo />;

        // Category: Attachments
        case "DEMO_StoreAttachment":
            return <StoreAttachmentDemo />;

        // Category: Revisions
        case "DEMO_EnableRevisions":
            return <EnableRevisionsDemo />;
            
        // Category: Queries
        case "DEMO_QueryOverview":
            return <QueryOverviewDemo />;
        case "DEMO_FullCollectionQuery":
            return <FullCollectionQueryDemo />;
        case "DEMO_QueryByDocumentId":
            return <QueryByDocumentIdDemo />;
        case "DEMO_FilteringQueryResults":
            return <FilteringQueryResultsDemo />;
        case "DEMO_ProjectingIndividualFields":
            return <ProjectingIndividualFieldsDemo />;
        case "DEMO_ProjectingUsingFunctions":
            return <ProjectingUsingFunctionsDemo />;

        // Category: Static Indexes
        case "DEMO_StaticIndexesOverview":
            return <StaticIndexesOverviewDemo />;
        case "DEMO_MapIndex":
            return <MapIndexDemo />;

        // Category: Text Search
        case "DEMO_FullTextSearchWithStaticIndex":
            return <FullTextSearchWithStaticIndexDemo />;
            
        // Category: Advanced
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