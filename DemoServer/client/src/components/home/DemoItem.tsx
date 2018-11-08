import * as React from "react";
import { Link } from "react-router-dom";
import { DemoWithProgress } from "../../models/demoModels";

interface DemoItemProps {
    category: string;
    demo: DemoWithProgress;
}

export function DemoItem(props: DemoItemProps) {
    const { demo, category } = props;
    const additionalClass = demo.completed ? " done" : "";
    return <Link to={`/demos/${category}/${demo.slug}`} className={`demo-item${additionalClass}`}>
        <img src="../img/demo-item.png" />
        <div className="title">{demo.title}</div>
    </Link>;
}