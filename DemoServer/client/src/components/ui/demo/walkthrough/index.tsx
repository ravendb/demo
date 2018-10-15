import * as React from "react";
import { Parameters } from "../Parameters";
import * as mockup from "../mockup";
import { Code } from "../Code";

interface WalkthroughProps {
}

interface WalkthroughState {
}

export class Walkthrough extends React.Component<WalkthroughProps, WalkthroughState> {
    constructor(props) {
        super(props);

        this.state = {
        };
    }

    render() {
        return <div className="demo-body">
            <div id="demo-body-container">
                <div className="walkthrough-top"></div>
                <div id="demo-highlight">
                    <div id="highlight-top"></div>
                    <div id="highlight-right"></div>
                    <div id="highlight-bottom">
                        <div className="walkthrough-description">
                            <h3>Lorem ipsum dolor sit amet</h3>
                        </div>
                    </div>
                    <div id="highlight-left"></div>
                </div>
                <Parameters items={[
                    { type: "text", name: "searchQuery", placeholder: "John" },
                    { type: "date", name: "searchQuery", placeholder: "2011-09-29" },
                    { type: "number", datatype: "integer", name: "searchQuery", placeholder: "10" },
                    { type: "number", datatype: "float", name: "searchQuery", placeholder: "10.42792" }
                ]} />
                <Code language="csharp" {...mockup} highlightLinesRange={{ start: 15, end: 25 }} />
            </div>
        </div>;
    }
}