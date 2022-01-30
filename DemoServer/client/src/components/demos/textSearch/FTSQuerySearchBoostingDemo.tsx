import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "id",
        "notes"
    ]}
/>;

export const FTSQuerySearchBoostingDemo = () => <Demo
    paramDefinitions={[
        { inputType: "number", name: "boost1", placeholder: "100", paramKind: "text-param" },
        { inputType: "number", name: "boost2", placeholder: "20", paramKind: "text-param" },
        { inputType: "number", name: "boost3", placeholder: "5", paramKind: "text-param" }
    ]}    
    resultsComponents={resultsCreator}
/>;
