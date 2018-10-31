import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const Demo101 = () => <Demo
    title="Demo 101"
    description="This is the description of demo 101."
    paramDefinitions={[
        { type: "text", name: "FirstName", placeholder: "John" },
        { type: "text", name: "LastName", placeholder: "Doe" }
    ]}
    resultsComponents={resultsCreator}
/>;