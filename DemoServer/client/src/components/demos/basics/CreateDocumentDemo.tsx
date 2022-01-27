import * as React from "react";
import { Demo } from "../Demo";
import { DocumentCreated, ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <>
    <DocumentCreated />
    <ResultText />
</>;

export const CreateDocumentDemo = () => <Demo
    paramDefinitions = {[
        { inputType: "text", name: "companyName", placeholder: "Hibernating Rhinos", paramKind: "text-param" },
        { inputType: "text", name: "companyPhone", placeholder: "(+972)52-5486969", paramKind: "text-param" },
        { inputType: "text", name: "contactName", placeholder: "New Contact Name", paramKind: "text-param" },
        { inputType: "text", name: "contactTitle", placeholder: "New Contact Title", paramKind: "text-param" }
    ]}
    resultsComponents = { resultsCreator }
/>;
