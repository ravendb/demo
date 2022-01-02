import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "firstName",
        "phone",
        "location"
    ]}
/>;

export const ProjectIndexResultsDemo = () => <Demo
    paramDefinitions={[
        { inputType: "number", name: "startYear", placeholder: "1993", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
