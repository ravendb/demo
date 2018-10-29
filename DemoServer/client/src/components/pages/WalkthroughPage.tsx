import * as React from "react";
import { Page } from "../Layout";
import { Walkthrough } from "../demoDisplay/walkthrough";
import { MockSidebar } from "../mockup";

export class WalkthroughPage extends React.Component {
  displayName = WalkthroughPage.name

  render() {
    return <Page>
      <MockSidebar />
      <Walkthrough />
    </Page>;
  }
}