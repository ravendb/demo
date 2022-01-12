type Basic = 
      "the-document-store"
    | "the-session"
    | "create-document"
    | "load-and-edit-document"
    | "delete-document";

type RelatedDocuments = 
      "create-related-documents"
    | "load-related-documents"
    | "query-related-documents"
    | "index-related-documents";

type Attachments = 
      "store-attachment"
    | "load-attachment"
    | "index-attachment-details";

type Revisions = 
      "enable-revisions"
    | "get-revisions";

type Queries = 
      "query-overview"
    | "query-example"
    | "full-collection-query"
    | "query-by-document-id"
    | "filtering-results-basics"
    | "filtering-results-multiple-conditions"
    | "projecting-individual-fields"
    | "projecting-using-functions"
    | "sorting-query-results"
    | "paging-query-results";

type StaticIndexes = 
      "static-indexes-overview"
    | "map-index"
    | "map-reduce-index"
    | "project-index-results" 
    | "store-fields-on-index";

type MultiMapIndexes =
      "multi-map-index-basic"
    | "multi-map-index-customized-fields";

type AutoIndexes = 
      "auto-map-index1"
    | "auto-map-index2"
    | "auto-map-reduce-index";

type FacetedSearch =
      "facets-basic"
    | "facets-options"
    | "facets-aggregations"
    | "facets-from-document"

type TextSearch = 
      "fts-with-static-index-single-field"
    | "fts-with-static-index-multiple-fields";

type CompareExchange =
      "create-compare-exchange"
    | "index-compare-exchange";

type Advanced = 
      "replication-failover"
    | "create-database";

export type DemoSlug = 
      Basic
    | RelatedDocuments
    | Attachments
    | Revisions
    | Queries
    | StaticIndexes
    | MultiMapIndexes
    | AutoIndexes
    | FacetedSearch
    | TextSearch
    | CompareExchange
    | Advanced;

export type CategorySlug = 
      "basics"
    | "related-documents"
    | "attachments"
    | "revisions"
    | "queries"
    | "static-indexes"
    | "multi-map-indexes"
    | "auto-indexes"
    | "faceted-search"
    | "text-search"
    | "compare-exchange"
    | "advanced";
