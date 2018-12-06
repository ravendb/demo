import * as React from "react";
import { Layout } from "../layout";
import { Home } from "../home";

export class HomePage extends React.Component {
  displayName = HomePage.name

  render() {
    return <Layout noContainer>
      <Home />
    </Layout>;
  }
}
