import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "id",
        "name",
        "description"
    ]}
/>;

export const FTSWithStaticIndexSingleFieldDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "searchTerm", placeholder: "pasta", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
