import * as React from "react";
import { Layout } from "../layout";
import { Home } from "../home";

export class HomePage extends React.Component {
  public render() {
    return <Layout noContainer>
      <Home />
    </Layout>;
  }
}
