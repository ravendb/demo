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

export const HighlightQueryResultsCustomizedDemo = () => <Demo
    paramDefinitions={[
        { inputType: "number", name: "fragmentLength", placeholder: "100", paramKind: "text-param" },
        { inputType: "number", name: "fragmentCount", placeholder: "1", paramKind: "text-param" },
        { inputType: "text", name: "tag1", placeholder: "+++", paramKind: "text-param" },
        { inputType: "text", name: "tag2", placeholder: "+++", paramKind: "text-param" },
        { inputType: "text", name: "tag3", placeholder: "<<<", paramKind: "text-param" },
        { inputType: "text", name: "tag4", placeholder: ">>>", paramKind: "text-param" },
    ]}
    resultsComponents={resultsCreator}
/>;
