import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const IncludeDocumentsWhenLoadingDemo = () => <Demo
    paramDefinitions = {[
        { inputType: "number", name: "pricePerUnit", placeholder: "New price per unit", paramKind: "text-param" },
        { inputType: "text", name: "phone", placeholder: "New phone number", paramKind: "text-param" }
    ]}
    resultsComponents = {resultsCreator}
/>;
