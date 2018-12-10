type Basic = "DEMO_TheDocumentStore" |
             "DEMO_CreateDocument"   |
             "DEMO_EditDocument"     |
             "DEMO_DeleteDocument";

type Attachments = "DEMO_StoreAttachment";

type Revisions = "DEMO_EnableRevisions";

type Queries = "DEMO_QueryOnCollection";

type Advanced = "DEMO_CreateDatabase"

export type DemoType = Basic | Attachments | Revisions | Queries | Advanced;
