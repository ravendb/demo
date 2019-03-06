type Basic = "the-document-store"
    | "the-session"
    | "create-document"
    | "edit-document"
    | "delete-document";

type RelatedDocuments = "create-related-documents"
    | "load-related-documents"
    | "query-related-documents"
    | "index-related-documents";

type Attachments = "store-attachment";

type Revisions = "enable-revisions"
    | "get-revisions";

type Queries = "query-overview"
    | "query-example"
    | "full-collection-query"
    | "query-by-document-id"
    | "filtering-results-basics"
    | "filtering-results-multiple-conditions"
    | "projecting-individual-fields"
    | "projecting-using-functions"
    | "sorting-query-results";

type StaticIndexes = "static-indexes-overview"
    | "map-index"
    | "map-reduce-index";

type AutoIndexes = "auto-map-index1"
    | "auto-map-index2";

type TextSearch = "fts-with-static-index-single-field"
    | "fts-with-static-index-multiple-fields";

type Advanced = "replication-failover"
    | "create-database";

export type DemoSlug = Basic
    | RelatedDocuments
    | Attachments
    | Revisions
    | Queries
    | StaticIndexes
    | AutoIndexes
    | TextSearch
    | Advanced;

export type CategorySlug = "basics"
    | "related-documents"
    | "attachments"
    | "revisions"
    | "queries"
    | "static-indexes"
    | "auto-indexes"
    | "text-search"
    | "advanced";
