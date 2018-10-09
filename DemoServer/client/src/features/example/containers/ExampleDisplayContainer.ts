import * as actions from "../actions";
import { connect } from "react-redux";
import { ExampleDisplay } from "../components/ExampleDisplay";
import { AppState } from "../../../store/state";
import { DemoAsyncDispatch } from "../../../store/async";

export function mapStateToProps({ example }: AppState) {
    return {
        loading: example.loading,
        text: example.text
    };
}

export function mapDispatchToProps(dispatch: DemoAsyncDispatch) {
    return {
        handleDataLoad: () => dispatch(actions.fetchResult())
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(ExampleDisplay);