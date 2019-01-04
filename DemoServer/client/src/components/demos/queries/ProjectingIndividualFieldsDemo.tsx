import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "companyName",
        "city",
        "country"
    ]}
/>;

export const ProjectingIndividualFieldsDemo = () => <Demo
    resultsComponents={resultsCreator}
/>;
