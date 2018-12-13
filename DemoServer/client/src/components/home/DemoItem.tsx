import * as React from "react";
import { Link } from "react-router-dom";
import * as classNames from "classnames";
import { DemoWithProgress } from "../../models/demoModels";

interface DemoItemProps {
    category: string;
    demo: DemoWithProgress;
}

export function DemoItem(props: DemoItemProps) {
    const { demo, category } = props;

    const className = classNames("demo-item", {
        "done": demo.completed
    });

    return <Link to={`/demos/${category}/${demo.slug}`} className={className}>
        <img src="../img/demo-item.png" />
        <div className="title">{demo.title}</div>
    </Link>;
}