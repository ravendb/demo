import * as React from "react";
import { Page } from "../Layout";
import { Example } from "../example/Example";

export class ExamplePage extends React.Component<{}, {}> {
    render() {
        return <Page>
          <Example /> 
        </Page>;
    }
}