import * as React from "react";
import { Demo } from "../Demo";
import { ResultText  } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const SimpleDocumentQueryDemo = () => <Demo
    paramDefinitions = {[
        { inputType: "text", name: "employeeDocumentID", placeholder: "employees/1-a", paramKind: "text-param" }
    ]}
    resultsComponents = { resultsCreator }
/>;
