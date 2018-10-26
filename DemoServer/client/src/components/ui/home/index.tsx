import * as React from "react";
import { DemoCategory, DemoWithProgress } from "./models";
import * as categories from "../../../demos/categories";
import { Category } from "../../../demos/categories";
import { Link } from "react-router-dom";

interface DemoItemProps {
    category: string;
    demo: DemoWithProgress;
}

function DemoItem(props: DemoItemProps) {
    const { demo, category } = props;
    const additionalClass = demo.completed ? " done" : "";
    return <Link to={`/demos/${category}/${demo.slug}`} className={`demo-item${additionalClass}`}>
        <img src="../img/demo-item.png" />
        <div className="title">{demo.title}</div>
    </Link>;
}

interface DemoCategoryProps {
    category: Category;
}

function DemoCategory(props: DemoCategoryProps) {
    const { title, slug, demos } = props.category;

    const demosWithProgress = demos.map(x => {
        return {
            ...x,
            completed: false
        } as DemoWithProgress;
    });

    return <div className="demo-category">
        <h2>{title}</h2>
        <div className="demo-group">
            {demosWithProgress.map((x, i) => <DemoItem category={slug} demo={x} key={`demo_item_${i}`} />)}
        </div>
    </div>;
}

interface HomeProps {
}

export class Home extends React.Component<HomeProps, {}> {
    render() {
        return <>
            <div className="header-image"><h1>Dive into RavenDB</h1></div>
            <div className="demo-list">
                {categories.categoryList.map((x, i) => <DemoCategory category={x} key={`demo_category_${i}`} />)}
            </div>
        </>;
    }
}