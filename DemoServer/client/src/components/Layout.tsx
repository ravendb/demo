import * as React from "react";
import { Header } from "./ui/Header";

export class Layout extends React.Component {
    displayName = Layout.name

    render() {
        return <>
            <Header />
            <div className="container">
                {this.props.children}
            </div>
        </>;
    }
}

export class Page extends React.Component<{}, {}> {
    render() {
        return <Layout>{this.props.children}</Layout>;
    }
}