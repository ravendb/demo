import * as React from "react";
import { Demo } from "../Demo";
import {ResultTable} from "../../demoDisplay/results/resultItems";

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

export const IndexAttachmentDetailsDemo = () => <Demo
    paramDefinitions = {[
        { inputType: "text", name: "attachmentContentType", placeholder: "image/jpeg", paramKind: "text-param" },
        { inputType: "number", name: "attachmentSize", placeholder: "18000", paramKind: "text-param" }
    ]}
    resultsComponents = { resultsCreator }
/>;
