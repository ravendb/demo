import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <>   
    <ResultText />
</>;

export const DeleteDocumentDemo = () => <Demo
    paramDefinitions = {[
        { type: "text", name: "documentID", placeholder: "companies/1-A" }
    ]}
    resultsComponents = {resultsCreator}
/>;
