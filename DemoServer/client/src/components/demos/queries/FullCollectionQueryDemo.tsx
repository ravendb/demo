import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "id",
        "externalId",
        "name",
        "phone",
        "fax"
    ]}
/>;

export const FullCollectionQueryDemo = () => <Demo
    resultsComponents={resultsCreator}
/>;
