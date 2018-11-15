import * as React from "react";
import { Demo } from "../Demo";
import { DocumentCreated } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <>
    <DocumentCreated />   
</>;

const demoDescription = "Create a new C# entity and save it as a document in the Database." +
    "The document will be stored in the RavenDB database in as JSON format object." +
    "The JSON structure is set according to the C# object model passed to the Session." +
    "(See the link about data modeling in the Assets below)";

export const CreateDocumentDemo = () => <Demo
    title = "Create Document"
    description = { demoDescription} 
    paramDefinitions = {[
        { type: "text", name: "companyName", placeholder: "Hibernating Rhinos" },
        { type: "text", name: "companyPhone", placeholder: "(+972)52-5486969" },
        { type: "text", name: "contactName", placeholder: "Contact Name Holder" },
        { type: "text", name: "contactTitle", placeholder: "Contact Title Holder" }
    ]}
    resultsComponents = { resultsCreator }
/>;
