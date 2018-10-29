import * as React from "react";
import { Page } from "../Layout";
import { MockupDemoBody, MockSidebar } from "../mockup";

export class DetailsPage extends React.Component {
  displayName = DetailsPage.name

  render() {
    return <Page>
      <MockSidebar />
      <MockupDemoBody />
    </Page>;
  }
}
