import * as React from "react";
import { Demo } from "../Demo";
import { JsonResult } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <JsonResult id="json-results" />;

export const FacetsOptionsDemo = () => <Demo
    paramDefinitions={[
        { inputType: "number", name: "start", placeholder: "3", paramKind: "text-param" },
        { inputType: "number", name: "pageSize", placeholder: "2", paramKind: "text-param" },
        { inputType: "text", name: "includeRemainingTerms", placeholder: "true", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
