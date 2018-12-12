import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const StoreAttachmentDemo = () => <Demo
    paramDefinitions = {[
        { inputType: "text", name: "documentID", placeholder: "companies/2-A", paramKind: "text-param" },
        { name: "attachment", paramKind: "file-upload-param" },
        { inputType: "text", name: "attachmentName", placeholder: "file.png", paramKind: "text-param" },
        { inputType: "text", name: "contentType", placeholder: "image/png", paramKind: "text-param" },
    ]}
    resultsComponents = { resultsCreator }
/>;
