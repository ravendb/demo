import * as React from "react";

interface DescriptionProps {
    title: string;
}

export class Description extends React.Component<DescriptionProps, {}> {
    render() {
        const { title, children } = this.props;
        return <>
            <h2>{title}</h2>
            <hr />
            {children}
        </>;
    }
}