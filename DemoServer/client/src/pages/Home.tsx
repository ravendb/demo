import * as React from "react";
import { Page } from "../components/Layout";

export class Home extends React.Component {
  displayName = Home.name

  render() {
    return <Page>
      <h4>Welcome!</h4>
    </Page>;
  }
}
