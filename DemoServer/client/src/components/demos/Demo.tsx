import * as React from "react";
import { Page } from "../Layout";
import { Sidebar } from "../demoDisplay/sidebar";
import { DemoBody, DemoBodyOwnProps } from "../demoDisplay/body";
import { AppState } from "../../store/state";
import { DemoThunkDispatch } from "../../store";
import { getMetadata } from "../../store/actions/demoActions";
import { connect } from "react-redux";
import { Spinner } from "../ui/Spinner";

export type DemoOwnProps = DemoBodyOwnProps;

export interface DemoStateProps {
    categorySlug: string;
    demoSlug: string;
    loading: boolean;
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
        const { loading } = this.props;

        return <Page>
            <Spinner show={loading} />
            <Sidebar {...this.props} />
            <DemoBody {...this.props} />
        </Page>;
    }
}

function mapStateToProps({ demos }: AppState): DemoStateProps {
    const { categorySlug, demoSlug, finishedLoadingDemo, finishedSettingPrerequisites } = demos;
    const finishedLoading = finishedLoadingDemo && finishedSettingPrerequisites;

    return {
        categorySlug,
        demoSlug,
        loading: !finishedLoading
    };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): DemoDispatchProps {
    return {
        loadMetadata: (category: string, demo: string) => dispatch(getMetadata(category, demo))
    };
}

export const Demo = connect<DemoStateProps, DemoDispatchProps, DemoOwnProps>(mapStateToProps, mapDispatchToProps)(DemoDisplay);