import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "id",
        "name",
        "supplier",
        "category" 
    ]}
/>;

export const IndexRelatedDocumentsDemo = () => <Demo
    paramDefinitions = {[
        { inputType: "text", name: "categoryName", placeholder: "Produce", paramKind: "text-param" }
    ]}    
    resultsComponents = {resultsCreator}
/>;
