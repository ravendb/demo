import * as React from "react";
import { Page } from "../components/Layout";
import { Home } from "../components/ui/home";

export class HomePage extends React.Component {
  displayName = HomePage.name

  render() {
    return <Page noContainer>
      <Home />
    </Page>;
  }
}
