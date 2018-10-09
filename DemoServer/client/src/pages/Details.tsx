import * as React from "react";
import { Page } from "../components/Layout";
import { Sidebar } from "../components/ui/sidebar";
import { DemoBody } from "../components/ui/demo/body";

export class Details extends React.Component {
  displayName = Details.name

  render() {
    return <Page>
      <Sidebar />
      <DemoBody />
    </Page>;
  }
}
