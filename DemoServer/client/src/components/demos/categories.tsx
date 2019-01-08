import { DemoType } from "./demoTypes";

export interface DemoInfo {
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
            slug: "the-document-store",
            title: "The Document Store",
            type: "DEMO_TheDocumentStore"
        },
        {
            slug: "the-session",
            title: "The Session",
            type: "DEMO_TheSession"
        },
        {
            slug: "create-document",
            title: "Create Document",
            type: "DEMO_CreateDocument"
        },
        {
            slug: "edit-document",
            title: "Edit Document",
            type: "DEMO_EditDocument"
        },
        {
            slug: "delete-document",
            title: "Delete Document",
            type: "DEMO_DeleteDocument"
        },
        {
            slug: "related-documents",
            title: "Related Documents",
            type: "DEMO_RelatedDocuments"
        }
    ]
};

const attachmentsCategory: Category = {
    slug: "attachments",
    title: "Attachments",
    demos: [
        {
            slug: "store-attachment",
            title: "Store Attachment",
            type: "DEMO_StoreAttachment"
        }
    ]
};

const revisionsCategory: Category = {
    slug: "revisions",
    title: "Revisions",
    demos: [
        {
            slug: "enable-revisions",
            title: "Enable Revisions",
            type: "DEMO_EnableRevisions"
        }
    ]
};

const queriesCategory: Category = {
    slug: "queries",
    title: "Queries",
    demos: [
        {
            slug: "query-overview",
            title: "Query Overview",
            type: "DEMO_QueryOverview"
        },
        {
            slug: "full-collection-query",
            title: "Full Collection Query",
            type: "DEMO_FullCollectionQuery"
        },
        {
            slug: "query-by-document-id",
            title: "Query by Document ID",
            type: "DEMO_QueryByDocumentId"
        },
        {
            slug: "filtering-results-basics",
            title: "Filtering Results - Basics",
            type: "DEMO_FilteringResultsBasics"
        },
        {
            slug: "filtering-results-multiple-conditions",
            title: "Filtering Results - Multiple Conditions",
            type: "DEMO_FilteringResultsMultipleConditions"
        },
        {
            slug: "projecting-individual-fields",
            title: "Projecting Individual Fields",
            type: "DEMO_ProjectingIndividualFields"
        },
        {
            slug: "projecting-using-functions",
            title: "Projecting Using Functions",
            type: "DEMO_ProjectingUsingFunctions"
        }
    ]
};

const staticIndexesCategory: Category = {
    slug: "static-indexes",
    title: "Static Indexes",
    demos: [
        {
            slug: "static-indexes-overview",
            title: "Static Indexes Overview",
            type: "DEMO_StaticIndexesOverview"
        },
        {
            slug: "map-index",
            title: "Map Index",
            type: "DEMO_MapIndex"
        },
        {
            slug: "map-reduce-index",
            title: "Map Reduce Index",
            type: "DEMO_MapReduceIndex"
        }
    ]
};

const autoIndexesCategory: Category = {
    slug: "auto-indexes",
    title: "Auto Indexes",
    demos: [
        {
            slug: "auto-map-index1",
            title: "Auto Map Index I",
            type: "DEMO_AutoMapIndex1"
        },
        {
            slug: "auto-map-index2",
            title: "Auto Map Index II",
            type: "DEMO_AutoMapIndex2"
        }
    ]
};

const textSearch: Category = {
    slug: "text-search",
    title: "Text Search",
    demos: [
        {
            slug: "fts-with-static-index-single-field",
            title: "FTS with Static Index - Single Field",
            type: "DEMO_FTSWithStaticIndexSingleField"
        },
        {
            slug: "fts-with-static-index-multiple-fields",
            title: "FTS with Static Index - Multiple Fields",
            type: "DEMO_FTSWithStaticIndexMultipleFields"
        }
    ]
};

const advancedCategory: Category = {
    slug: "advanced",
    title: "Advanced",
    demos: [
        {
            slug: "replication-failover",
            title: "Replication Failover",
            type: "DEMO_ReplicationFailover"
        },
        {
            slug: "create-database",
            title: "Create Database",
            type: "DEMO_CreateDatabase"
        }
    ]
};

export const categoryList: Category[] = [
    basicsCategory,
    attachmentsCategory,
    revisionsCategory,
    queriesCategory,
    staticIndexesCategory,
    autoIndexesCategory,
    textSearch,
    advancedCategory
];
