import * as React from "react";
import { Parameters } from "../Parameters";
import { Code } from "../Code";
import { NavPanel } from "./NavPanel";
import { Results } from "./Results";
import * as mockup from "../../mockup";

interface DemoBodyProps {
}

interface DemoBodyState {
}

export class DemoBody extends React.Component<DemoBodyProps, DemoBodyState> {
    constructor(props) {
        super(props);

        this.state = {
        };
    }

    render() {
        const resultsId = "results";
        return <div className="demo-body">
            <div id="demo-body-container">
                <Parameters items={[
                    { type: "text", name: "searchQuery", placeholder: "John" },
                    { type: "date", name: "searchQuery", placeholder: "2011-09-29" },
                    { type: "number", datatype: "integer", name: "searchQuery", placeholder: "10" },
                    { type: "number", datatype: "float", name: "searchQuery", placeholder: "10.42792" }
                ]} />
                <Code language="csharp" {...mockup} />
                <NavPanel
                    onWalkthroughClick={() => alert("WALKTHROUGH clicked")}
                    onRunScriptClicked={() => console.log("RUN SCRIPT clicked")}
                    resultsElementId={resultsId}
                />
                <Results
                    elementId={resultsId}
                    clientExecTime="0.06 seconds"
                    serverExecTime="< 0.01 seconds"
                />
            </div>
        </div>;
    }
}