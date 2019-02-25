import * as React from "react";
import { DemoItem } from "./DemoItem";
import { DemoProgress } from "../../models/progress";
import { DemoWithProgress } from "../../models/demo";
import { CategoryWithDemoVersions } from "../../models/dtos/context";

interface DemoCategoryProps {
    category: CategoryWithDemoVersions;
    completedDemos: DemoProgress[];
}

export function DemoCategory(props: DemoCategoryProps) {
    const { completedDemos, category } = props;
    const { title, slug, demos } = category;

    const demosWithProgress = demos.map(d => {
        const completedDemo = completedDemos && completedDemos.find(c => c.demo === d.slug);
        return {
            ...d,
            completed: !!completedDemo
        } as DemoWithProgress;
    });

    return <div className="demo-category">
        <h2>{title}</h2>
        <div className="demo-group">
            {demosWithProgress.map((x, i) => <DemoItem category={slug} demo={x} key={`demo_item_${i}`} />)}
        </div>
    </div>;
}
