import * as React from "react";
import { Demo } from "../Demo";
import { JsonResult } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <JsonResult id="json-results" />;

export const QueryByDocumentIdDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "employeeDocumentID", placeholder: "employees/1-A", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
