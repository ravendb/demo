import * as React from "react";
import { Page } from "../Layout";
import { Sidebar } from "../ui/sidebar";
import { Walkthrough } from "../ui/demo/walkthrough";

export class WalkthroughPage extends React.Component {
  displayName = WalkthroughPage.name

  render() {
    return <Page>
      <Sidebar />
      <Walkthrough />
    </Page>;
  }
}