import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const FullTextSearchWithStaticIndexDemo = () => <Demo
    paramDefinitions = {[
        { inputType: "text", name: "someFood", placeholder: "pasta", paramKind: "text-param" }
    ]}
    resultsComponents = { resultsCreator }
/>;
