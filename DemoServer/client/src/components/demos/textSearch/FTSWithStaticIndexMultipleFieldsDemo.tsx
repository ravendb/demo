import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "id",
        "artist",
        "title",
        "trackId",
    ]}
/>;

export const FTSWithStaticIndexMultipleFieldsDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "searchTerm", placeholder: "Floyd", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
