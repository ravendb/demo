import * as React from "react";

interface DescriptionProps {
}

export class Description extends React.Component<DescriptionProps, {}> {
    render() {
        const { children } = this.props;
        return <>
            <h2>Description</h2>
            <hr />
            {children}
        </>;
    }
}