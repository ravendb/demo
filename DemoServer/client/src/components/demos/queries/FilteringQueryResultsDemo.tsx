import * as React from "react";
import { Demo } from "../Demo";
import { ResultText  } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const FilteringQueryResultsDemo = () => <Demo
    paramDefinitions = {[
        { inputType: "text", name: "country", placeholder: "USA", paramKind: "text-param" }
    ]}
    resultsComponents = { resultsCreator }
/>;
