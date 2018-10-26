import * as React from "react";
import { Route } from "react-router";
import { HomePage } from "./components/pages/HomePage";
import { ExamplePage } from "./components/pages/ExamplePage";
import { DetailsPage } from "./components/pages/DetailsPage";
import { WalkthroughPage } from "./components/pages/WalkthroughPage";
import { DemoFactory } from "./components/demos/DemoFactory";

export default class App extends React.Component<{}, {}> {
  displayName = App.name

  constructor(props) {
    super(props);
  }

  render() {
    return <>
      <Route exact path='/' component={HomePage} />
      <Route exact path='/example' component={ExamplePage} />
      <Route exact path='/details' component={DetailsPage} />
      <Route path='/demos/:category/:demo' render={props => <DemoFactory categorySlug={props.match.params.category} demoSlug={props.match.params.demo} />} />
      <Route exact path='/walkthrough' component={WalkthroughPage} />
    </>;
  }
}
