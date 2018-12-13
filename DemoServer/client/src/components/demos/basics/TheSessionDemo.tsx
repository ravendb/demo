import * as React from "react";
import { Demo } from "../Demo";
import { DocumentCreated } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <DocumentCreated />;

export const TheSessionDemo = () => <Demo
    resultsComponents = { resultsCreator }
/>;
