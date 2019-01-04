import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "firstName",
        "lastName"
    ]}
/>;

export const QueryOverviewDemo = () => <Demo
    resultsComponents={resultsCreator}
/>;
