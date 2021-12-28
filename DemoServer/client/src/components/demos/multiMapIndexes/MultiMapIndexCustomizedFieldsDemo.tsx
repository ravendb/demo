import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "collection",
        "contactName",
        "contactTitle",
        "phone"
    ]}
/>;

export const MultiMapIndexCustomizedFieldsDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "namePrefix", placeholder: "Michael", paramKind: "text-param" },
        { inputType: "text", name: "titlePrefix", placeholder: "Sales", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
