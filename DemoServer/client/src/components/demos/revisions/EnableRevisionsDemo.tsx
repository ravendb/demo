import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <>   
    <ResultText />
</>;

export const EnableRevisionsDemo = () => <Demo
    paramDefinitions = {[
        { type: "text", name: "collection1", placeholder: "Companies" },
        { type: "text", name: "collection2", placeholder: "Orders" }
    ]}
    resultsComponents = { resultsCreator }
/>;
