import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <>   
    <ResultText />
</>;

export const StoreAttachmentDemo = () => <Demo
    paramDefinitions = {[
        { type: "text", name: "documentID", placeholder: "companies/2-A" },
        { type: "text", name: "attachmentType1", placeholder: "image/jpeg" },
        { type: "text", name: "attachmentType2", placeholder: "video/mp4" }
    ]}
    fileUploadDefinitions = {[
        { name: "attachment1" },
        { name: "attachment2" }
    ]}
    resultsComponents = { resultsCreator }
/>;
