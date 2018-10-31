import * as React from "react";
import { Page } from "../Layout";
import { Sidebar, SidebarOwnProps } from "../sidebar";
import { DemoBodyDisplay } from "../demoDisplay/body";
import { AppState } from "../../store/state";
import { DemoAsyncDispatch } from "../../store/async";
import { getMetadata } from "../../actions/demoActions";
import { connect } from "react-redux";
import { ParameterItem } from "../demoDisplay/Parameters";

export interface DemoOwnProps extends SidebarOwnProps {
    parameters: ParameterItem[];
}

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
        const { parameters } = this.props;
        return <Page>
            <Sidebar {...this.props} />
            <DemoBodyDisplay parameters={parameters} />
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