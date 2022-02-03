import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "id",
        "notes"
    ]}
/>;

export const FTSQuerySearchOperatorsDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "term1", placeholder: "Spanish", paramKind: "text-param" },
        { inputType: "text", name: "term2", placeholder: "Portuguese", paramKind: "text-param" },
        { inputType: "text", name: "term3", placeholder: "Manager", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
