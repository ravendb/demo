import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const IncludeDocumentsWhenLoadingDemo = () => <Demo
    paramDefinitions = {[
        { inputType: "number", name: "pricePerUnit", placeholder: "12", paramKind: "text-param" },
        { inputType: "text", name: "phone", placeholder: "(+972)52-5486969", paramKind: "text-param" }
    ]}
    resultsComponents = {resultsCreator}
/>;
