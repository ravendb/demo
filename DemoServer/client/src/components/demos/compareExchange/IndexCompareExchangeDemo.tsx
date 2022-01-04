import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "id",
        "name"
    ]}
/>;

export const IndexCompareExchangeDemo = () => <Demo
    paramDefinitions={[
        { inputType: "number", name: "minValue", placeholder: "25", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
