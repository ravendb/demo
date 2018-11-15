import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <>   
    <ResultText />
</>;

const demoDescription = "Edit a document that is already stored in the database." +
    "You will load an existing document from the Data Store, update it and save it back.";

export const EditDocumentDemo = () => <Demo
    title = "Edit Document"
    description = { demoDescription }
    paramDefinitions = {[
        { type: "text", name: "documentID", placeholder: "companies/1-A" },
        { type: "text", name: "companyName", placeholder: "New Company Name" }
    ]}
    resultsComponents = {resultsCreator}
/>;
