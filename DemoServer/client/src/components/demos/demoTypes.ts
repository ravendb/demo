type Basic = "DEMO_CreateDocument" | 
             "DEMO_EditDocument"   |
             "DEMO_DeleteDocument";

type Attachments = "DEMO_StoreAttachment";

type Queries = "DEMO_SimpleQuery";

type Advanced = "DEMO_CreateDatabase"

export type DemoType = Basic | Attachments | Queries | Advanced;
