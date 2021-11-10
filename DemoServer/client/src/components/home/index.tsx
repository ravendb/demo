import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../store/state";
import { DemoThunkDispatch } from "../../store";
import { DemoCategory } from "./DemoCategory";
import { UserProgress } from "../../models/progress";
import { getContext } from "../../store/actions/demo";
import { Spinner } from "../ui/Spinner";
import { CategoryWithDemoVersions } from "../../models/dtos/context";
import { IconStudio } from "../helpers/icons";

interface StateProps {
    progress: UserProgress;
    loading: boolean;
    categories: CategoryWithDemoVersions[];
}

interface DispatchProps {
    getContext: () => void;
}

type Props = StateProps & DispatchProps;

class HomeComponent extends React.Component<Props, {}> {

    public componentDidMount() {
        this.props.getContext();
    }

    private _getCategoryElement(category: CategoryWithDemoVersions, index: number) {
        const { progress } = this.props;
        const completedForCategory = progress
            && progress.completedDemos
            && progress.completedDemos.filter(x => x.category === category.slug);

        return <DemoCategory key={`demo_category_${index}`}
            category={category}
            completedDemos={completedForCategory}
        />;
    }

    private _demoList() {
        const { categories } = this.props;

        return <div className="demo-list">
            {categories && categories.map((x, i) => this._getCategoryElement(x, i))}
        </div>;
    }

    private _studioCta() {
        return <div className="open-in-studio">
            <h3>Implement on Your Own Sample Database Now</h3>
            <a href="http://live-test.ravendb.net" id="openStudio" className="fab" target="_blank">
                <IconStudio /> <span>Open studio</span>
            </a>
        </div>
    }

    public render() {
        const { loading } = this.props;

        return <>
            <div className="header-image"><h1>RavenDB Step-By-Step Coding Walkthrough</h1></div>
            <Spinner show={loading} />
            {!loading && this._demoList()}
            {this._studioCta()}
        </>;
    }
}

export const Home = connect<StateProps, DispatchProps, {}>(
    ({ demos }: AppState): StateProps => {
        const { userProgress, loadingContext: loadingMainPage, categoriesWithVersions } = demos;

        return {
            progress: userProgress,
            loading: loadingMainPage,
            categories: categoriesWithVersions
        };
    },
    (dispatch: DemoThunkDispatch): DispatchProps => ({
        getContext: () => dispatch(getContext())
    })
)(HomeComponent);
