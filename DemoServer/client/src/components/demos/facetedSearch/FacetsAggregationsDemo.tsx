import * as React from "react";
import { Demo } from "../Demo";
import { JsonResult } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <JsonResult id="json-results" />;

export const FacetsAggregationsDemo = () => <Demo
    paramDefinitions={[
        { inputType: "number", name: "range1", placeholder: "25", paramKind: "text-param" },
        { inputType: "number", name: "range2", placeholder: "50", paramKind: "text-param" },
        { inputType: "number", name: "range3", placeholder: "100", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
