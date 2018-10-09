import * as React from "react";
import { Route } from "react-router";
import { Home } from "./pages/Home";
import { ExamplePage } from "./pages/Example";
import { Details } from "./pages/Details";

export default class App extends React.Component<{}, {}> {
  displayName = App.name

  constructor(props) {
    super(props);
  }

  render() {
    return <>
      <Route exact path='/' component={Home} />
      <Route exact path='/example' component={ExamplePage} />
      <Route exact path='/details' component={Details} />
    </>;
  }
}
