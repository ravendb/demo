import * as React from "react";
import { Category, categoryList } from "../demos/categories";
import { connect } from "react-redux";
import { AppState } from "../../store/state";
import { DemoThunkDispatch } from "../../store";
import { DemoCategory } from "./DemoCategory";
import { UserProgress } from "../../models/progress";
import { getProgress } from "../../store/actions/progressActions";

interface HomeStateProps {
    progress: UserProgress;
}

interface HomeDispatchProps {
    getProgress: () => void;
}

type HomeProps = HomeStateProps & HomeDispatchProps;

class HomeDisplay extends React.Component<HomeProps, {}> {
    componentDidMount() {
        const { getProgress } = this.props;
        getProgress();
    }

    getCategoryElement(category: Category, index: number) {
        const { progress } = this.props;
        const completedForCategory = progress
            && progress.completedDemos
            && progress.completedDemos.filter(x => x.category === category.slug);

        return <DemoCategory category={category} key={`demo_category_${index}`} completedDemos={completedForCategory} />
    }

    render() {
        return <>
            <div className="header-image"><h1>Dive into RavenDB</h1></div>
            <div className="demo-list">
                {categoryList.map((x, i) => this.getCategoryElement(x, i))}
            </div>
        </>;
    }
}

export const Home = connect<HomeStateProps, HomeDispatchProps, {}>(
    ({ progress }: AppState): HomeStateProps => {
        return {
            progress: progress.userProgress
        }
    },
    (dispatch: DemoThunkDispatch): HomeDispatchProps => {
        return {
            getProgress: () => dispatch(getProgress())
        }
    }
)(HomeDisplay);