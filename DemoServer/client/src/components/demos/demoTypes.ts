type Basic = "DEMO_TheDocumentStore" |
             "DEMO_TheSession"       | 
             "DEMO_CreateDocument"   |
             "DEMO_EditDocument"     |
             "DEMO_DeleteDocument";

type Attachments = "DEMO_StoreAttachment";

type Revisions = "DEMO_EnableRevisions";

type Queries = "DEMO_SimpleCollectionQuery" |
               "DEMO_SimpleDocumentQuery"   |
               "DEMO_FilteringQueryResults";

type Advanced = "DEMO_CreateDatabase"

export type DemoType = Basic | Attachments | Revisions | Queries | Advanced;
