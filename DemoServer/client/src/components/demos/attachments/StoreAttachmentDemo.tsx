import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <>   
    <ResultText />
</>;

export const StoreAttachmentDemo = () => <Demo
    paramDefinitions = {[
        { type: "text", name: "documentID", placeholder: "companies/2-A" },
        { type: "text", name: "attachment1", placeholder: "CompanyLogo.jpg" },
        { type: "text", name: "attachment2", placeholder: "CompanyVideo.mp4" },
        { type: "text", name: "attachmentType1", placeholder: "image/jpeg" },
        { type: "text", name: "attachmentType2", placeholder: "video/mp4" }
    ]}
    resultsComponents = { resultsCreator }
/>;
