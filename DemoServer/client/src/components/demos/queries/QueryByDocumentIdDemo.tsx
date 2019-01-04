import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "id",
        "lastName",
        "firstName",
        "title",
        "homePhone",
        "extension"
    ]}
/>;

export const QueryByDocumentIdDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "employeeDocumentID", placeholder: "employees/1-a", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
