import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "cityName", 
        "numberOfCompaniesInCity",
        "numberOfSuppliersInCity",
        "numberOfOrdersShippedToCity"
    ]}
/>;

export const MultiMapReduceIndexDemo = () => <Demo
    paramDefinitions={[
        { inputType: "number", name: "minCompaniesCount", placeholder: "5", paramKind: "text-param" },
        { inputType: "number", name: "minOrdersCount", placeholder: "80", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
