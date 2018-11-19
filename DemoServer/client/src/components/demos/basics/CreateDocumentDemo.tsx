import * as React from "react";
import { Demo } from "../Demo";
import { DocumentCreated } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <>
    <DocumentCreated />   
</>;

export const CreateDocumentDemo = () => <Demo
    paramDefinitions = {[
        { type: "text", name: "companyName", placeholder: "Hibernating Rhinos" },
        { type: "text", name: "companyPhone", placeholder: "(+972)52-5486969" },
        { type: "text", name: "contactName", placeholder: "Contact Name Holder" },
        { type: "text", name: "contactTitle", placeholder: "Contact Title Holder" }
    ]}
    resultsComponents = { resultsCreator }
/>;
