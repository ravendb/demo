import * as React from "react";
import { DemoCategory, DemoWithProgress } from "./models";
import * as mockup from "../../mockup";

interface DemoItemProps {
    demo: DemoWithProgress;
}

function DemoItem(props: DemoItemProps) {
    const { name, img, completed } = props.demo;
    const additionalClass = completed ? " done" : "";
    return <a className={`demo-item${additionalClass}`} href="details.html">
        <img src={img} />
        <div className="title">{name}</div>
    </a>;
}

interface GroupProps {
    demos: DemoWithProgress[];
}

function Group(props: GroupProps) {
    const { demos } = props;
    return <div className="demo-group">
        {demos.map((x, i) => <DemoItem demo={x} key={`demo_item_${i}`} />)}
    </div>;
}

interface CategoryProps {
    category: DemoCategory;
}

function Category(props: CategoryProps) {
    const { name, demos } = props.category;
    return <div className="demo-category">
        <h2>{name}</h2>
        <Group demos={demos} />
    </div>;
}

interface DemoListProps {
    categories: DemoCategory[];
}

function DemoList(props: DemoListProps) {
    const { categories } = props;
    return <div className="demo-list">
        {categories.map((x, i) => <Category category={x} key={`demo_category_${i}`} />)}
    </div>;
}

interface HomeProps {
}

export class Home extends React.Component<HomeProps, {}> {
    render() {
        return <>
            <div className="header-image"><h1>Dive into RavenDB</h1></div>
            <DemoList categories={mockup.demoCategories} />
        </>;
    }
}