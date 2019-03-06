import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[       
        "unitsInStock",
        "name",
        "id"
    ]}
/>;

export const SortingQueryResultsDemo = () => <Demo
    paramDefinitions={[
        { inputType: "number", name: "numberOfUnits", placeholder: "20", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
