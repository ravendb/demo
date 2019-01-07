type Basic = "DEMO_TheDocumentStore" |
             "DEMO_TheSession"       | 
             "DEMO_CreateDocument"   |
             "DEMO_EditDocument"     |
             "DEMO_DeleteDocument";

type Attachments = "DEMO_StoreAttachment";

type Revisions = "DEMO_EnableRevisions";

type Queries = "DEMO_QueryOverview"              | 
               "DEMO_FullCollectionQuery"        |
               "DEMO_QueryByDocumentId"          |
               "DEMO_FilteringQueryResults"      |
               "DEMO_ProjectingIndividualFields" |
               "DEMO_ProjectingUsingFunctions"   |
               "DEMO_Query1" |
               "DEMO_Query2";

type StaticIndexes = "DEMO_StaticIndexesOverview" |
                     "DEMO_MapIndex"              |
                     "DEMO_MapReduceIndex";

type TextSearch = "DEMO_FullTextSearchWithStaticIndex";

type Advanced = "DEMO_ReplicationFailover"
                |"DEMO_CreateDatabase";

export type DemoType = Basic | Attachments | Revisions | Queries | StaticIndexes | TextSearch | Advanced;
