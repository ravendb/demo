import * as React from "react";
import { Header } from "./ui/Header";

interface LayoutProps {
    noContainter?: boolean;
}

export class Layout extends React.Component<LayoutProps, {}> {
    displayName = Layout.name

    render() {
        const { noContainter, children } = this.props;

        const body = noContainter
            ? <>{children}</>
            : <div className="container">
                {children}
            </div>;

        return <>
            <Header />
            {body}
        </>;
    }
}

interface PageProps extends LayoutProps {
}

export class Page extends React.Component<PageProps, {}> {
    render() {
        return <Layout {...this.props}>{this.props.children}</Layout>;
    }
}