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

const queriesCategory: Category = {
    slug: "queries",
    title: "Queries",
    demos: [
        {
            slug: "simple-collection-query",
            title: "Simple Collection Query",
            type: "DEMO_SimpleCollectionQuery"
        },
        {
            slug: "simple-document-query",
            title: "Simple Document Query",
            type: "DEMO_SimpleDocumentQuery"
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

export const categoryList: Category[] = [
    basicsCategory,
    attachmentsCategory,
    revisionsCategory,
    queriesCategory,
    advancedCategory
];
