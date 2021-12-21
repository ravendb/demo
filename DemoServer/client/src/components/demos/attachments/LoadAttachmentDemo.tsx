import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const LoadAttachmentDemo = () => <Demo
    paramDefinitions = {[
        { inputType: "text", name: "documentID", placeholder: "categories/1-A", paramKind: "text-param" },
        { inputType: "text", name: "attachmentName", placeholder: "image.jpg", paramKind: "text-param" }
    ]}
    resultsComponents = { resultsCreator }
/>;
