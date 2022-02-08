import * as React from "react";
import { Demo } from "../Demo";
import { ResultTable } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultTable
    fields={[
        "artist",
        "title"
    ]}
/>;

export const FTSQuerySearchWildcardsDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "start", placeholder: "ma", paramKind: "text-param" },
        { inputType: "text", name: "end", placeholder: "lin", paramKind: "text-param" },
        { inputType: "text", name: "middle", placeholder: "oliv", paramKind: "text-param" },
        { inputType: "number", name: "numberOfResults", placeholder: "10", paramKind: "text-param" }
    ]}    
    resultsComponents={resultsCreator}
/>;
