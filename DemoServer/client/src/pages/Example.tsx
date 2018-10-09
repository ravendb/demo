import * as React from "react";
import { Page } from "../components/Layout";
import Example from "../features/example/containers/ExampleDisplayContainer";

export class ExamplePage extends React.Component<{}, {}> {
    render() {
        return <Page>
          <Example /> 
        </Page>;
    }
}