import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "employeeName",
        "longitude",
        "latitude",
    ]}
/>;

export const SpatialQueryDemo = () => <Demo
    paramDefinitions={[
        { inputType: "number", name: "radius", placeholder: "2", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
