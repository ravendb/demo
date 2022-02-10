import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "artist",
        "songFragment",
    ]}
/>;

export const HighlightQueryResultsMapReduceDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "searchTerm", placeholder: "smile", paramKind: "text-param" },
        { inputType: "text", name: "preTag", placeholder: " (: ", paramKind: "text-param" },
        { inputType: "text", name: "postTag", placeholder: " :) ", paramKind: "text-param" },
        { inputType: "number", name: "fragmentLength", placeholder: "80", paramKind: "text-param" },
        { inputType: "number", name: "fragmentCount", placeholder: "1", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
