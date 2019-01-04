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
            slug: "query1",
            title: "Query I",
            type: "DEMO_Query1"
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
            slug: "filtering-query-results",
            title: "Filtering Query Results",
            type: "DEMO_FilteringQueryResults"
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

const textSearch: Category = {
    slug: "text-search",
    title: "Text Search",
    demos: [
        {
            slug: "full-text-search-with-static-index",
            title: "Full Text Search with Static Index",
            type: "DEMO_FullTextSearchWithStaticIndex"
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
    attachmentsCategory,
    revisionsCategory,
    queriesCategory,
    staticIndexesCategory,
    textSearch,
    advancedCategory
];
