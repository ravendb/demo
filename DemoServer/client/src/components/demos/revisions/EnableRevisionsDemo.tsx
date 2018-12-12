import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const EnableRevisionsDemo = () => <Demo
    paramDefinitions = {[
        { inputType: "text", name: "collection1", placeholder: "Companies", paramKind: "text-param" },
        { inputType: "text", name: "collection2", placeholder: "Orders", paramKind: "text-param" }
    ]}
    resultsComponents = { resultsCreator }
/>;
