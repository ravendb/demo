import * as React from "react";

interface ExampleDisplayProps {
    loading: boolean;
    text: string;
    handleDataLoad: () => void;
}

export class ExampleDisplay extends React.Component<ExampleDisplayProps, {}> {
    componentDidMount() {
        this.props.handleDataLoad();
    }

    render() {
        const { loading, text } = this.props;
        const body = loading ? "Loading Result..." : `Result: ${text}`;

        return <>
            <h4>Example Page</h4>
            <p>{body}</p>
        </>;
    }
}