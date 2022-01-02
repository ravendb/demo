import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "title",
        "fullName"
    ]}
/>;

export const ProjectingUsingFunctionsDemo = () => <Demo
    resultsComponents={resultsCreator}
/>;
