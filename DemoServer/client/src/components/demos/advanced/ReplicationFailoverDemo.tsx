import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const ReplicationFailoverDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "machineName", placeholder: null, paramKind: "text-param" },
        { inputType: "text", name: "id", placeholder: "Users/1", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
