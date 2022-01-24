import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "id",
        "orderedAt"
    ]}
/>;

export const FanoutIndexDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "namePrefix", placeholder: "Chocolade", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
