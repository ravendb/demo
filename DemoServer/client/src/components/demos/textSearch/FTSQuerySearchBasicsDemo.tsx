import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "id",
        "notes"
    ]}
/>;

export const FTSQuerySearchBasicsDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "term1", placeholder: "Washington", paramKind: "text-param" },
        { inputType: "text", name: "term2", placeholder: "Colorado", paramKind: "text-param" }
    ]}    
    resultsComponents={resultsCreator}
/>;
