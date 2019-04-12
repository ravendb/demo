import * as React from "react";
import { Header } from "./header";
import { ErrorMessage } from "./dialogs/ErrorMessage";
import { ResetDatabaseConfirm } from "./dialogs/ResetDatabaseConfirm";
import { ResetProgressConfirm } from "./dialogs/ResetProgressConfirm";
import { Metadata } from "./Metadata";
import { TrackingDialog } from "./tracking/TrackingDialog";

interface LayoutProps {
    noContainer?: boolean;
}

export class Layout extends React.Component<LayoutProps, {}> {

    private _dialogs() {
        return <>
            <ErrorMessage />
            <ResetDatabaseConfirm />
            <ResetProgressConfirm />
            <TrackingDialog />
        </>;
    }

    public render() {
        const { noContainer, children } = this.props;

        const body = noContainer
            ? <>{children}</>
            : <div className="container">
                {children}
            </div>;

        return <>
            <Metadata />
            <Header />
            {body}
            {this._dialogs()}
        </>;
    }
}
