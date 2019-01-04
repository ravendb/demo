import * as React from "react";
import { Demo } from "../Demo";
import { ResultText  } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const Query2Demo = () => <Demo
    paramDefinitions = {[
        { inputType: "text", name: "firstName", placeholder: "Robert", paramKind: "text-param" }
    ]}
    resultsComponents = { resultsCreator }
/>;
