import * as React from "react";
import { Demo } from "../Demo";
import { ResultText  } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <>   
    <ResultText />
</>;

export const SimpleQueryDemo = () => <Demo
    title = "Simple Query"
    description = "Simple Query Demo Descdription."
    paramDefinitions = { [] }
    resultsComponents = { resultsCreator }
/>;
