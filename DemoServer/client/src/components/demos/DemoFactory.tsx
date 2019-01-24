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
import { CategorySlug, DemoSlug } from "../../models/slugs";

const DemoNotFound = () => {
    return <>
        <h2>Demo not found</h2>
        <p>We're sorry, this is not the demo you're looking for.</p>
    </>;
}

interface Props {
    categorySlug: CategorySlug;
    demoSlug: DemoSlug;
}

export class DemoFactory extends React.Component<Props, {}> {

    getBasicsDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "the-document-store":
                return <TheDocumentStoreDemo />;
            case "the-session":
                return <TheSessionDemo />;
            case "create-document":
                return <CreateDocumentDemo />;
            case "edit-document":
                return <EditDocumentDemo />;
            case "delete-document":
                return <DeleteDocumentDemo />;
        }
    }

    getRelatedDocumentsDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "create-related-documents":
                return <CreateRelatedDocumentsDemo />;
            case "load-related-documents":
                return <LoadRelatedDocumentsDemo />;
            case "query-related-documents":
                return <QueryRelatedDocumentsDemo />;
            case "index-related-documents":
                return <IndexRelatedDocumentsDemo />;
        }
    }

    getAttachmentsDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "store-attachment":
                return <StoreAttachmentDemo />;
        }
    }

    getRevisionsDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "enable-revisions":
                return <EnableRevisionsDemo />;
            case "get-revisions":
                return <GetRevisionsDemo />;
        }
    }

    getQueriesDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "query-overview":
                return <QueryOverviewDemo />;
            case "query-example":
                return <QueryExampleDemo />;
            case "full-collection-query":
                return <FullCollectionQueryDemo />;
            case "query-by-document-id":
                return <QueryByDocumentIdDemo />;
            case "filtering-results-basics":
                return <FilteringResultsBasicsDemo />;
            case "filtering-results-multiple-conditions":
                return <FilteringResultsMultipleConditionsDemo />;
            case "projecting-individual-fields":
                return <ProjectingIndividualFieldsDemo />;
            case "projecting-using-functions":
                return <ProjectingUsingFunctionsDemo />;
        }
    }

    getStaticIndexesDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "static-indexes-overview":
                return <StaticIndexesOverviewDemo />;
            case "map-index":
                return <MapIndexDemo />;
            case "map-reduce-index":
                return <MapReduceIndexDemo />;
        }
    }

    getAutoIndexesDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "auto-map-index1":
                return <AutoMapIndex1Demo />;
            case "auto-map-index2":
                return <AutoMapIndex2Demo />;
        }
    }

    getTextSearchDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "fts-with-static-index-single-field":
                return <FTSWithStaticIndexSingleFieldDemo />;
            case "fts-with-static-index-multiple-fields":
                return <FTSWithStaticIndexMultipleFieldsDemo />;
        }
    }

    getAdvancedDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "replication-failover":
                return <ReplicationFailoverDemo />;
            case "create-database":
                return <CreateDatabaseDemo />;
        }
    }

    getDemoForCategory(categorySlug: CategorySlug, demoSlug: DemoSlug) {
        switch (categorySlug) {
            case "basics":
                return this.getBasicsDemo(demoSlug);
            case "related-documents":
                return this.getRelatedDocumentsDemo(demoSlug);
            case "attachments":
                return this.getAttachmentsDemo(demoSlug);
            case "revisions":
                return this.getRevisionsDemo(demoSlug);
            case "queries":
                return this.getQueriesDemo(demoSlug);
            case "static-indexes":
                return this.getStaticIndexesDemo(demoSlug);
            case "auto-indexes":
                return this.getAutoIndexesDemo(demoSlug);
            case "text-search":
                return this.getTextSearchDemo(demoSlug);
            case "advanced":
                return this.getAdvancedDemo(demoSlug);
        }

        return null;
    }

    render() {
        const { categorySlug, demoSlug } = this.props;

        if (!categorySlug && !demoSlug) {
            return null;
        }

        const demo = this.getDemoForCategory(categorySlug, demoSlug);
        return demo || <DemoNotFound />;
    }
}
