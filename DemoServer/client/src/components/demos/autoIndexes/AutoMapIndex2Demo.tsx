import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "id",
        "lastName",
        "firstName",
        "title",
        "homePhone",
        "extension"
    ]}
/>;

export const AutoMapIndex2Demo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "country", placeholder: "UK", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
