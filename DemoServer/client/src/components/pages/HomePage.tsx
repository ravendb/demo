import * as React from "react";
import { Page } from "../Layout";
import { Home } from "../home/Home";

export class HomePage extends React.Component {
  displayName = HomePage.name

  render() {
    return <Page noContainer>
      <Home />
    </Page>;
  }
}
