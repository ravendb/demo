import * as React from "react";
import { Demo } from "../Demo";
import {ResultTable, ResultText} from "../../demoDisplay/results/resultItems";


const resultsCreator = () => <ResultTable
    fields={[
        "id",
        "name",
        "phone"
    ]}
/>;

export const GetRevisionsDemo = () => <Demo
    resultsComponents = { resultsCreator }
/>;
