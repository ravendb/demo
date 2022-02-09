import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "totalItemsOrdered",
        "orderedAt"
    ]}
/>;

export const StoreFieldsInIndexDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "companyID", placeholder: "companies/1-A", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
