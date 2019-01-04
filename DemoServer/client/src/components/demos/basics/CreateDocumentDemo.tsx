import * as React from "react";
import { Demo } from "../Demo";
import { DocumentCreated, ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <>   
    <DocumentCreated />
    <ResultText />
</>;

export const CreateDocumentDemo = () => <Demo
    paramDefinitions = {[
        { inputType: "text", name: "supplierName", placeholder: "Hibernating Rhinos", paramKind: "text-param" },
        { inputType: "text", name: "supplierPhone", placeholder: "(+972)52-5486969", paramKind: "text-param" },
        { inputType: "text", name: "productName", placeholder: "Beverly Hills Cop", paramKind: "text-param" }
    ]}
    resultsComponents = { resultsCreator }
/>;
