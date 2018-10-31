import * as React from "react";
import { Route } from "react-router";
import { HomePage } from "./components/pages/HomePage";
import { DemoFactory } from "./components/demos/DemoFactory";
import { demoPath } from "./utils/paths";

export default class App extends React.Component<{}, {}> {
  displayName = App.name

  constructor(props) {
    super(props);
  }

  render() {
    return <>
      <Route exact path='/' component={HomePage} />
      <Route path={demoPath} component={DemoFactory} />
    </>;
  }
}
