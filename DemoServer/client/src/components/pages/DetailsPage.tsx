import * as React from "react";
import { Page } from "../Layout";
import { Sidebar } from "../ui/sidebar";
import { DemoBody } from "../demoDisplay/body";

export class DetailsPage extends React.Component {
  displayName = DetailsPage.name

  render() {
    return <Page>
      <Sidebar />
      <DemoBody />
    </Page>;
  }
}
