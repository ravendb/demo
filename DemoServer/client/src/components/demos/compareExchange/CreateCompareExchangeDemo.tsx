import * as React from "react";
import { Demo } from "../Demo";
import { ResultText} from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const CreateCompareExchangeDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "cmpXchgKey", placeholder: "abc@gmail.com", paramKind: "text-param" },
        { inputType: "text", name: "cmpXchgValue", placeholder: "employee/1-A", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
