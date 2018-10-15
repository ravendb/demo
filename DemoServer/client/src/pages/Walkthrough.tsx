import * as React from "react";
import { Page } from "../components/Layout";
import { Sidebar } from "../components/ui/sidebar";
import { Walkthrough } from "../components/ui/demo/walkthrough";

export class WalkthroughPage extends React.Component {
  displayName = WalkthroughPage.name

  render() {
    return <Page>
      <Sidebar />
      <Walkthrough />
    </Page>;
  }
}