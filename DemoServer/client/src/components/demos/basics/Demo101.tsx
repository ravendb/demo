import * as React from "react";
import { Demo } from "../Demo";
import { ResultText, FileAdded } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <>
    <FileAdded />
    <ResultText />
</>;

export const Demo101 = () => <Demo
    title="Demo 101"
    description="This is the description of demo 101."
    paramDefinitions={[
        { type: "text", name: "firstName", placeholder: "John" },
        { type: "text", name: "lastName", placeholder: "Doe" }
    ]}
    resultsComponents={resultsCreator}
/>;