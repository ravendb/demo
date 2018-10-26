import * as React from "react";

export interface DemoProps {
}

export abstract class Demo extends React.Component<DemoProps, {}> {
}

export class DemoNotFound extends Demo {
    render() {
        return <>
            <h2>Demo not found</h2>
            <p>We're sorry, this is not the demo you're looking for.</p>
        </>;
    }
}