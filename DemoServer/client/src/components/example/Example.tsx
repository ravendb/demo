import * as React from "react";
import { AppState } from "../../store/state";
import { DemoAsyncDispatch } from "../../store/async";
import * as exampleActions from "../../actions/exampleActions";
import { connect } from "react-redux";

interface ExampleDisplayProps {
    loading: boolean;
    text: string;
    handleDataLoad: () => void;
}

class ExampleDisplay extends React.Component<ExampleDisplayProps, {}> {
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

export function mapStateToProps({ example }: AppState) {
    return {
        loading: example.loading,
        text: example.text
    };
}

export function mapDispatchToProps(dispatch: DemoAsyncDispatch) {
    return {
        handleDataLoad: () => dispatch(exampleActions.fetchResult())
    }
}

export const Example = connect(mapStateToProps, mapDispatchToProps)(ExampleDisplay);