import * as React from "react";
import { Header } from "./header";
import { ErrorMessage } from "./ErrorMessage";

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
            </div>;

        return <>
            <Header />
            {body}
            <ErrorMessage />
        </>;
    }
}
