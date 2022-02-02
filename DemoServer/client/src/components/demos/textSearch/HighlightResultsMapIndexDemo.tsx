import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "indexField",
        "fragment",
        "documentId"
    ]}
/>;

export const HighlightResultsMapIndexDemo = () => <Demo
    paramDefinitions={[
        { inputType: "number", name: "fragmentLength", placeholder: "50", paramKind: "text-param" },
        { inputType: "number", name: "fragmentCount", placeholder: "2", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
