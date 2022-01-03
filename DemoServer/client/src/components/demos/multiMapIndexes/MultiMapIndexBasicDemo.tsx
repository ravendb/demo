import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "name"
    ]}
/>;

export const MultiMapIndexBasicDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "namePrefix", placeholder: "A", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
