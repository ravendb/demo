import * as React from "react";
import { Page } from "../Layout";
import { Sidebar, SidebarOwnProps } from "../sidebar";
import { DemoBody, DemoBodyOwnProps } from "../demoDisplay/body";
import { AppState } from "../../store/state";
import { DemoAsyncDispatch } from "../../store/async";
import { getMetadata } from "../../store/actions/demoActions";
import { connect } from "react-redux";

export type DemoOwnProps = SidebarOwnProps & DemoBodyOwnProps;

export interface DemoStateProps {
    categorySlug: string;
    demoSlug: string;
}

export interface DemoDispatchProps {
    loadMetadata: (category: string, demo: string) => void;
}

export type DemoProps = DemoStateProps & DemoOwnProps & DemoDispatchProps;

export class DemoDisplay extends React.Component<DemoProps, {}> {
    componentDidMount() {
        const { loadMetadata, categorySlug, demoSlug } = this.props;
        loadMetadata(categorySlug, demoSlug);
    }

    render() {
        return <Page>
            <Sidebar {...this.props} />
            <DemoBody {...this.props} />
        </Page>;
    }
}

function mapStateToProps({ demos }: AppState): DemoStateProps {
    const { categorySlug, demoSlug } = demos;
    return {
        categorySlug,
        demoSlug
    };
}

function mapDispatchToProps(dispatch: DemoAsyncDispatch): DemoDispatchProps {
    return {
        loadMetadata: (category: string, demo: string) => dispatch(getMetadata(category, demo))
    };
}

export const Demo = connect<DemoStateProps, DemoDispatchProps, DemoOwnProps>(mapStateToProps, mapDispatchToProps)(DemoDisplay);