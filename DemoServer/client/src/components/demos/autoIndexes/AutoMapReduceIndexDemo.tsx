import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "country",
        "numberOfEmployees"
    ]}
/>;

export const AutoMapReduceIndexDemo = () => <Demo    
    resultsComponents={resultsCreator}
/>;
