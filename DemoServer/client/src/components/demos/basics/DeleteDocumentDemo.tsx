import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const DeleteDocumentDemo = () => <Demo
    paramDefinitions = {[
        { inputType: "text", name: "documentID", placeholder: "companies/1-A", paramKind: "text-param" }
    ]}
    resultsComponents = {resultsCreator}
/>;
