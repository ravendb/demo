import * as React from "react";
import { DemoFactoryProps } from "./DemoFactory";

export interface DemoOwnProps extends DemoFactoryProps {
}

export interface DemoStateProps {
}

export interface DemoDispatchProps {
    loadMetadata: (category: string, demo: string) => void;
}

export type DemoProps = DemoStateProps & DemoOwnProps & DemoDispatchProps;

export abstract class DemoDisplay extends React.Component<DemoProps, {}> {
}

export class DemoNotFound extends DemoDisplay {
    render() {
        return <>
            <h2>Demo not found</h2>
            <p>We're sorry, this is not the demo you're looking for.</p>
        </>;
    }
}