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
        { inputType: "text", name: "contactName", placeholder: "Contact Name Holder", paramKind: "text-param" },
        { inputType: "text", name: "contactTitle", placeholder: "Contact Title Holder", paramKind: "text-param" }
    ]}
    resultsComponents = { resultsCreator }
/>;
