import * as React from "react";
import { CreateDatabaseDemo } from "./advanced/CreateDatabaseDemo";
import { TheDocumentStoreDemo } from "./basics/TheDocumentStoreDemo";
import { TheSessionDemo } from "./basics/TheSessionDemo";
import { CreateDocumentDemo } from "./basics/CreateDocumentDemo";
import { LoadAndEditDocumentDemo } from "./basics/LoadAndEditDocumentDemo";
import { DeleteDocumentDemo } from "./basics/DeleteDocumentDemo";
import { CreateRelatedDocumentsDemo } from "./relatedDocuments/CreateRelatedDocumentsDemo";
import { LoadRelatedDocumentsDemo } from "./relatedDocuments/LoadRelatedDocumentsDemo";
import { QueryRelatedDocumentsDemo } from "./relatedDocuments/QueryRelatedDocumentsDemo";
import { IndexRelatedDocumentsDemo } from "./relatedDocuments/IndexRelatedDocumentsDemo";
import { StoreAttachmentDemo } from "./attachments/StoreAttachmentDemo";
import { LoadAttachmentDemo } from "./attachments/LoadAttachmentDemo";
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
import { SortingQueryResultsDemo } from "./queries/SortingQueryResultsDemo";
import { PagingQueryResultsDemo } from "./queries/PagingQueryResultsDemo";
import { StaticIndexesOverviewDemo } from "./staticIndexes/StaticIndexesOverviewDemo";
import { MapIndexDemo } from "./staticIndexes/MapIndexDemo";
import { MultiMapIndexBasicDemo } from "./multiMapIndexes/MultiMapIndexBasicDemo";
import { MultiMapIndexCustomizedFieldsDemo } from "./multiMapIndexes/MultiMapIndexCustomizedFieldsDemo";
import { MapReduceIndexDemo } from "./staticIndexes/MapReduceIndexDemo";
import { ProjectIndexResultsDemo } from "./staticIndexes/ProjectIndexResultsDemo";
import { StoreFieldsOnIndexDemo } from "./staticIndexes/StoreFieldsOnIndexDemo";
import { AutoMapIndex1Demo } from "./autoIndexes/AutoMapIndex1Demo";
import { AutoMapIndex2Demo } from "./autoIndexes/AutoMapIndex2Demo";
import { AutoMapReduceIndexDemo } from "./autoIndexes/AutoMapReduceIndexDemo";
import { FTSWithStaticIndexSingleFieldDemo } from "./textSearch/FTSWithStaticIndexSingleFieldDemo";
import { FTSWithStaticIndexMultipleFieldsDemo } from "./textSearch/FTSWithStaticIndexMultipleFieldsDemo";
import { CreateCompareExchangeDemo } from "./compareExchange/CreateCompareExchangeDemo";
import { IndexCompareExchangeDemo } from "./compareExchange/IndexCompareExchangeDemo";
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

    private _getBasicsDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "the-document-store":
                return <TheDocumentStoreDemo />;
            case "the-session":
                return <TheSessionDemo />;
            case "create-document":
                return <CreateDocumentDemo />;
            case "load-and-edit-document":
                return <LoadAndEditDocumentDemo />;
            case "delete-document":
                return <DeleteDocumentDemo />;
        }
    }

    private _getRelatedDocumentsDemo(demoSlug: DemoSlug) {
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

    private _getAttachmentsDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "store-attachment":
                return <StoreAttachmentDemo />;
            case "load-attachment":
                return <LoadAttachmentDemo />;
        }
    }

    private _getRevisionsDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "enable-revisions":
                return <EnableRevisionsDemo />;
            case "get-revisions":
                return <GetRevisionsDemo />;
        }
    }

    private _getQueriesDemo(demoSlug: DemoSlug) {
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
            case "sorting-query-results":
                return <SortingQueryResultsDemo />;
            case "paging-query-results":
                return <PagingQueryResultsDemo />;
        }
    }

    private _getStaticIndexesDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "static-indexes-overview":
                return <StaticIndexesOverviewDemo />;
            case "map-index":
                return <MapIndexDemo />;
            case "map-reduce-index":
                return <MapReduceIndexDemo />;
            case "project-index-results":
                return <ProjectIndexResultsDemo />;
            case "store-fields-on-index":
                return <StoreFieldsOnIndexDemo />;
        }
    }

    private _getMultiMapIndexesDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "multi-map-index-basic":
                return <MultiMapIndexBasicDemo />;
            case "multi-map-index-customized-fields":
                return <MultiMapIndexCustomizedFieldsDemo />;
        }
    }

    private _getAutoIndexesDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "auto-map-index1":
                return <AutoMapIndex1Demo />;
            case "auto-map-index2":
                return <AutoMapIndex2Demo />;
            case "auto-map-reduce-index":
                return <AutoMapReduceIndexDemo />;
        }
    }

    private _getTextSearchDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "fts-with-static-index-single-field":
                return <FTSWithStaticIndexSingleFieldDemo />;
            case "fts-with-static-index-multiple-fields":
                return <FTSWithStaticIndexMultipleFieldsDemo />;
        }
    }

    private _getCompareExchangeDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "create-compare-exchange":
                return <CreateCompareExchangeDemo />;
            case "index-compare-exchange":
                return <IndexCompareExchangeDemo />;
        }
    }

    private _getAdvancedDemo(demoSlug: DemoSlug) {
        switch (demoSlug) {
            case "replication-failover":
                return <ReplicationFailoverDemo />;
            case "create-database":
                return <CreateDatabaseDemo />;
        }
    }

    private _getDemoForCategory(categorySlug: CategorySlug, demoSlug: DemoSlug) {
        switch (categorySlug) {
            case "basics":
                return this._getBasicsDemo(demoSlug);
            case "related-documents":
                return this._getRelatedDocumentsDemo(demoSlug);
            case "attachments":
                return this._getAttachmentsDemo(demoSlug);
            case "revisions":
                return this._getRevisionsDemo(demoSlug);
            case "queries":
                return this._getQueriesDemo(demoSlug);
            case "static-indexes":
                return this._getStaticIndexesDemo(demoSlug);
            case "multi-map-indexes":
                return this._getMultiMapIndexesDemo(demoSlug);
            case "auto-indexes":
                return this._getAutoIndexesDemo(demoSlug);
            case "text-search":
                return this._getTextSearchDemo(demoSlug);
            case "compare-exchange":
                return this._getCompareExchangeDemo(demoSlug);
            case "advanced":
                return this._getAdvancedDemo(demoSlug);
        }

        return null;
    }

    public render() {
        const { categorySlug, demoSlug } = this.props;

        if (!categorySlug && !demoSlug) {
            return null;
        }

        const demo = this._getDemoForCategory(categorySlug, demoSlug);
        return demo || <DemoNotFound />;
    }
}
