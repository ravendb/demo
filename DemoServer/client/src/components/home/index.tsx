import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../store/state";
import { DemoThunkDispatch } from "../../store";
import { DemoCategory } from "./DemoCategory";
import { UserProgress } from "../../models/progress";
import { getVersions } from "../../store/actions/demoActions";
import { Spinner } from "../ui/Spinner";
import { MainPageCategoryDto } from "../../models/dtos";

interface StateProps {
    progress: UserProgress;
    loading: boolean;
    categories: MainPageCategoryDto[];
}

interface DispatchProps {
    getDemoVersions: () => void;
}

type Props = StateProps & DispatchProps;

class HomeComponent extends React.Component<Props, {}> {

    componentDidMount() {
        this.props.getDemoVersions();
    }

    getCategoryElement(category: MainPageCategoryDto, index: number) {
        const { progress } = this.props;
        const completedForCategory = progress
            && progress.completedDemos
            && progress.completedDemos.filter(x => x.category === category.slug);

        return <DemoCategory key={`demo_category_${index}`}
            category={category}
            completedDemos={completedForCategory}
        />;
    }

    demoList() {
        const { categories } = this.props;

        return <div className="demo-list">
            {categories.map((x, i) => this.getCategoryElement(x, i))}
        </div>;
    }

    render() {
        const { loading } = this.props;

        return <>
            <div className="header-image"><h1>Dive into RavenDB</h1></div>
            <Spinner show={loading} />
            {!loading && this.demoList()}
        </>;
    }
}

export const Home = connect<StateProps, DispatchProps, {}>(
    ({ demos }: AppState): StateProps => {
        const { userProgress, loadingMainPage, categories } = demos;

        return {
            progress: userProgress,
            loading: loadingMainPage,
            categories
        };
    },
    (dispatch: DemoThunkDispatch): DispatchProps => ({
        getDemoVersions: () => dispatch(getVersions())
    })
)(HomeComponent);