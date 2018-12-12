import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const EditDocumentDemo = () => <Demo
    paramDefinitions = {[
        { inputType: "text", name: "companyName", placeholder: "New Company Name", paramKind: "text-param" }
    ]}
    resultsComponents = {resultsCreator}
/>;
