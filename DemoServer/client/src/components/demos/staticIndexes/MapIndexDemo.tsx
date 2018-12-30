import * as React from "react";
import { Demo } from "../Demo";
import { ResultText  } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const MapIndexDemo = () => <Demo
    paramDefinitions = {[
        { inputType: "number", name: "startYear", placeholder: "1993", paramKind: "text-param" }
    ]}    
    resultsComponents = { resultsCreator }
/>;
