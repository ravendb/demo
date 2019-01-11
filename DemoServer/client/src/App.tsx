import * as React from "react";
import { Route, Switch } from "react-router";
import { HomePage } from "./components/pages/HomePage";
import { DemoFactory } from "./components/demos/DemoFactory";
import { demoPath } from "./utils/paths";

export default class App extends React.Component<{}, {}> {
  displayName = App.name

  constructor(props) {
    super(props);
  }

  private getDemoFactory = (routeParams: any) => <DemoFactory
    categorySlug={routeParams.category}
    demoSlug={routeParams.demo}
  />;

  render() {
    return <Switch>
      <Route exact path='/' component={HomePage} />
      <Route path={demoPath} render={props => this.getDemoFactory(props.match.params)} />
    </Switch>;
  }
}
