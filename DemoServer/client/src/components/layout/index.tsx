import * as React from "react";
import { Header } from "./header";

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

            {/* <div id="errorMessage">
                <div className="message">
                    <h2>Oops... :(</h2>
                    <p>An connection error occurred, please reload and try again.</p>
                    <div className="text-center margin-top">
                        <button className="btn btn-default"><i className="icon-reload"></i> Reload</button>
                    </div>
                </div>
                <div className="overlay"></div>
            </div> */}
        </>;
    }
}
