import * as React from "react";
import { Route } from "react-router";
import { HomePage } from "./pages/Home";
import { ExamplePage } from "./pages/Example";
import { Details } from "./pages/Details";
import { WalkthroughPage } from "./pages/Walkthrough";
import { DemoFactory } from "./demos/DemoFactory";

export default class App extends React.Component<{}, {}> {
  displayName = App.name

  constructor(props) {
    super(props);
  }

  render() {
    return <>
      <Route exact path='/' component={HomePage} />
      <Route exact path='/example' component={ExamplePage} />
      <Route exact path='/details' component={Details} />
      <Route path='/demos/:category/:demo' render={props => <DemoFactory categorySlug={props.match.params.category} demoSlug={props.match.params.demo} />} />
      <Route exact path='/walkthrough' component={WalkthroughPage} />
    </>;
  }
}
