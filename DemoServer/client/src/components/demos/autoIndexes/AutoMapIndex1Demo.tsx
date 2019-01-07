import * as React from "react";
import { Demo } from "../Demo";
import { JsonResult } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <JsonResult id="json-results" />;

export const AutoMapIndex1Demo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "firstName", placeholder: "Steven", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
