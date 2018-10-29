import * as React from "react";
import { DemoDisplay, DemoStateProps, DemoDispatchProps, DemoOwnProps } from "../Demo";
import { Page } from "../../Layout";
import { Sidebar } from "../../sidebar";
import { AppState } from "../../../store/state";
import { DemoAsyncDispatch } from "../../../store/async";
import { getMetadata } from "../../../actions/demoActions";
import { connect } from "react-redux";

class Demo101Display extends DemoDisplay {
    componentDidMount() {
        const { loadMetadata, categorySlug, demoSlug } = this.props;
        loadMetadata(categorySlug, demoSlug);
    }

    render() {
        return <Page>
        <Sidebar title="Demo 101" description="This is the description of demo 101." />
      </Page>;
    }
}

function mapStateToProps({ demo }: AppState): DemoStateProps {
    return {
    };
}

function mapDispatchToProps(dispatch: DemoAsyncDispatch): DemoDispatchProps {
    return {
        loadMetadata: (category: string, demo: string) => dispatch(getMetadata(category, demo))
    }
}

export const Demo101 = connect<DemoStateProps, DemoDispatchProps, DemoOwnProps>(mapStateToProps, mapDispatchToProps)(Demo101Display);
