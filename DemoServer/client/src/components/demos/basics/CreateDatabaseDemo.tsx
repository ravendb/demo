import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <>   
    <ResultText />
</>;

const demoDescription = "Create a Database in RavenDB Server." +
                        "The database will hold your JSON documents." +
                        "If you have more than one server instance in your RavenDB cluster," +
                        "then you can set the database replication factor.";
                        // TODO - Enable working with multi line array

export const CreateDatabaseDemo = () => <Demo
    title = "Create Database"
    description = { demoDescription }    
    paramDefinitions = { [] }
    resultsComponents = { resultsCreator }
/>;
