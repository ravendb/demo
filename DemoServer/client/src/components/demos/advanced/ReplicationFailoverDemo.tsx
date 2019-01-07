import * as React from "react";
import { Demo } from "../Demo";
import { JsonResults } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <JsonResults id="replication-json-results" />;

export const ReplicationFailoverDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "machineName", placeholder: "localhost", paramKind: "text-param" },
        { inputType: "text", name: "id", placeholder: "employees/1-A", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
