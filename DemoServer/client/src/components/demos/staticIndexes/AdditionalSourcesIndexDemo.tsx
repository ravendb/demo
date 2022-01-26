import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "productName",
        "originalPrice",
        "salesPrice",
        "profitPrice"
    ]}
/>;

export const AdditionalSourcesIndexDemo = () => <Demo
    paramDefinitions={[
        { inputType: "number", name: "price", placeholder: "5", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
