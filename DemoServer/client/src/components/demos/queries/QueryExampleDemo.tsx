import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "employeeName",
        "title",
        "hiredAt",
        "managerName"        
    ]}
/>;

export const QueryExampleDemo = () => <Demo
    resultsComponents={resultsCreator}
/>;
