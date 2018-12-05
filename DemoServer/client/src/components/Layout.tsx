import * as React from "react";
import { Header } from "./ui/Header";

interface LayoutProps {
    noContainer?: boolean;
}

export class Layout extends React.Component<LayoutProps, {}> {
    displayName = Layout.name

    render() {
        const { noContainer, children } = this.props;

        const body = noContainer
            ? <>{children}</>
            : <div className="container">
                {children}
                <div className="toast">Link has been copied to clipboard</div>
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
