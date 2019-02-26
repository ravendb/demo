import * as React from "react";
import { Layout } from "../layout";
import { Sidebar } from "../demoDisplay/sidebar";
import { DemoBody, DemoBodyOwnProps } from "../demoDisplay/body";
import { AppState } from "../../store/state";
import { DemoThunkDispatch } from "../../store";
import { getMetadata } from "../../store/actions/demo";
import { connect } from "react-redux";
import { Spinner } from "../ui/Spinner";
import { ShareToast } from "../demoDisplay/toasts/ShareToast";
import { InvalidFileToast } from "../demoDisplay/toasts/InvalidFileToast";
import { CategorySlug, DemoSlug } from "../../models/slugs";
import { Language } from "../../models/common";

export type DemoOwnProps = DemoBodyOwnProps;

export interface DemoStateProps {
    language: Language;
    categorySlug: CategorySlug;
    demoSlug: DemoSlug;
    loading: boolean;
}

export interface DemoDispatchProps {
    loadMetadata: () => void;
}

export type DemoProps = DemoStateProps & DemoOwnProps & DemoDispatchProps;

export class DemoDisplay extends React.Component<DemoProps, {}> {

    private _downloadMetadata() {
        this.props.loadMetadata();
    }

    public componentDidMount() {
        const { categorySlug, demoSlug } = this.props;

        if (categorySlug && demoSlug) {
            this._downloadMetadata();
        }
    }

    public componentDidUpdate(prevProps: DemoProps) {
        const { categorySlug: prevCategory, demoSlug: prevDemo, language: prevLanguage } = prevProps;
        const { categorySlug, demoSlug, language } = this.props;

        if (categorySlug !== prevCategory || demoSlug !== prevDemo || language !== prevLanguage) {
            this._downloadMetadata();
        }
    }

    public render() {
        const { loading } = this.props;

        return <Layout>
            <Spinner show={loading} />
            <ShareToast />
            <InvalidFileToast />
            <Sidebar {...this.props} />
            <DemoBody {...this.props} />
        </Layout>;
    }
}

function mapStateToProps({ demos }: AppState): DemoStateProps {
    const { categorySlug, demoSlug, finishedLoadingDemo, finishedSettingPrerequisites, language } = demos;
    const finishedLoading = finishedLoadingDemo && finishedSettingPrerequisites;

    return {
        language,
        categorySlug,
        demoSlug,
        loading: !finishedLoading
    };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): DemoDispatchProps {
    return {
        loadMetadata: () => dispatch(getMetadata())
    };
}

export const Demo = connect<DemoStateProps, DemoDispatchProps, DemoOwnProps>(
    mapStateToProps,
    mapDispatchToProps
)(DemoDisplay);
