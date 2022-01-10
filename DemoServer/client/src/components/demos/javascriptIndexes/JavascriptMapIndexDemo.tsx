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

export const JavascriptMapIndexDemo = () => <Demo
    paramDefinitions={[
        { inputType: "number", name: "startYear", placeholder: "1993", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
