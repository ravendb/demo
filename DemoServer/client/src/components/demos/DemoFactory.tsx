import * as React from "react";
import { CreateDatabaseDemo } from "./advanced/CreateDatabaseDemo";
import { TheDocumentStoreDemo } from "./basics/TheDocumentStoreDemo";
import { TheSessionDemo } from "./basics/TheSessionDemo";
import { CreateDocumentDemo } from "./basics/CreateDocumentDemo";
import { EditDocumentDemo } from "./basics/EditDocumentDemo";
import { DeleteDocumentDemo } from "./basics/DeleteDocumentDemo";
import { CreateRelatedDocumentsDemo } from "./relatedDocuments/CreateRelatedDocumentsDemo";
import { LoadRelatedDocumentsDemo } from "./relatedDocuments/LoadRelatedDocumentsDemo";
import { QueryRelatedDocumentsDemo } from "./relatedDocuments/QueryRelatedDocumentsDemo";
import { IndexRelatedDocumentsDemo } from "./relatedDocuments/IndexRelatedDocumentsDemo";
import { StoreAttachmentDemo } from "./attachments/StoreAttachmentDemo";
import { EnableRevisionsDemo } from "./revisions/EnableRevisionsDemo";
import { GetRevisionsDemo } from "./revisions/GetRevisionsDemo";
import { QueryOverviewDemo } from "./queries/QueryOverviewDemo";
import { QueryExampleDemo } from "./queries/QueryExampleDemo";
import { FullCollectionQueryDemo } from "./queries/FullCollectionQueryDemo";
import { QueryByDocumentIdDemo } from "./queries/QueryByDocumentIdDemo";
import { FilteringResultsBasicsDemo } from "./queries/FilteringResultsBasicsDemo";
import { FilteringResultsMultipleConditionsDemo } from "./queries/FilteringResultsMultipleConditionsDemo";
import { ProjectingIndividualFieldsDemo } from "./queries/ProjectingIndividualFieldsDemo";
import { ProjectingUsingFunctionsDemo } from "./queries/ProjectingUsingFunctionsDemo";
import { StaticIndexesOverviewDemo } from "./staticIndexes/StaticIndexesOverviewDemo";
import { MapIndexDemo } from "./staticIndexes/MapIndexDemo";
import { MapReduceIndexDemo } from "./staticIndexes/MapReduceIndexDemo";
import { AutoMapIndex1Demo } from "./autoIndexes/AutoMapIndex1Demo";
import { AutoMapIndex2Demo } from "./autoIndexes/AutoMapIndex2Demo";
import { FTSWithStaticIndexSingleFieldDemo } from "./textSearch/FTSWithStaticIndexSingleFieldDemo";
import { FTSWithStaticIndexMultipleFieldsDemo } from "./textSearch/FTSWithStaticIndexMultipleFieldsDemo";
import { ReplicationFailoverDemo } from "./advanced/ReplicationFailoverDemo";
import { getDemoType } from "../../store/selectors/demos";

const DemoNotFound = () => {
    return <>
        <h2>Demo not found</h2>
        <p>We're sorry, this is not the demo you're looking for.</p>
    </>;
}

interface DemoFactoryProps {
    categorySlug: string;
    demoSlug: string;
}

export const DemoFactory = (props: DemoFactoryProps) => {
    const { categorySlug, demoSlug } = props;

    if (!categorySlug && !demoSlug) {
        return null;
    }

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

        // Category: Related Documents
        case "DEMO_CreateRelatedDocuments":
            return <CreateRelatedDocumentsDemo />;
        case "DEMO_LoadRelatedDocuments":
            return <LoadRelatedDocumentsDemo />;
        case "DEMO_QueryRelatedDocuments":
            return <QueryRelatedDocumentsDemo />;
        case "DEMO_IndexRelatedDocuments":
            return <IndexRelatedDocumentsDemo />;
            
        // Category: Attachments
        case "DEMO_StoreAttachment":
            return <StoreAttachmentDemo />;

        // Category: Revisions
        case "DEMO_EnableRevisions":
            return <EnableRevisionsDemo />;
        case "DEMO_GetRevisions":
            return <GetRevisionsDemo />;
            
        // Category: Queries
        case "DEMO_QueryOverview":
            return <QueryOverviewDemo />;
        case "DEMO_QueryExample":
            return <QueryExampleDemo />;
        case "DEMO_FullCollectionQuery":
            return <FullCollectionQueryDemo />;
        case "DEMO_QueryByDocumentId":
            return <QueryByDocumentIdDemo />;
        case "DEMO_FilteringResultsBasics":
            return <FilteringResultsBasicsDemo />;
        case "DEMO_FilteringResultsMultipleConditions":
            return <FilteringResultsMultipleConditionsDemo />;
        case "DEMO_ProjectingIndividualFields":
            return <ProjectingIndividualFieldsDemo />;
        case "DEMO_ProjectingUsingFunctions":
            return <ProjectingUsingFunctionsDemo />;       

        // Category: Static Indexes
        case "DEMO_StaticIndexesOverview":
            return <StaticIndexesOverviewDemo />;
        case "DEMO_MapIndex":
            return <MapIndexDemo />;
        case "DEMO_MapReduceIndex":
            return <MapReduceIndexDemo />;

        // Category: Auto Indexes
        case "DEMO_AutoMapIndex1":
            return <AutoMapIndex1Demo />;
        case "DEMO_AutoMapIndex2":
            return <AutoMapIndex2Demo />;
            
        // Category: Text Search
        case "DEMO_FTSWithStaticIndexSingleField":
            return <FTSWithStaticIndexSingleFieldDemo />;
        case "DEMO_FTSWithStaticIndexMultipleFields":
            return <FTSWithStaticIndexMultipleFieldsDemo />;
            
        // Category: Advanced
        case "DEMO_ReplicationFailover":
            return <ReplicationFailoverDemo />;
        case "DEMO_CreateDatabase":
            return <CreateDatabaseDemo />;   
    }

    return <DemoNotFound />;
}
