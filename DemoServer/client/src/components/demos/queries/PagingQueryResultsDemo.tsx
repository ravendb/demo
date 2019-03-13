import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";


const resultsCreator = () => <>
    <ResultTable
        fields={[
            "externalId",
            "name",
            "phone"
        ]}/>
</>;

export const PagingQueryResultsDemo = () => <Demo
    paramDefinitions={[
        { inputType: "number", name: "resultsToSkip", placeholder: "10", paramKind: "text-param" },
        { inputType: "number", name: "resultsToTake", placeholder: "5", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
