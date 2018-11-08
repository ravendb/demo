import * as React from "react";
import { DemoWithProgress, DemoProgressDto } from "../../models/demoModels";
import { Category } from "../demos/categories";
import { DemoItem } from "./DemoItem";

interface DemoCategoryProps {
    category: Category;
    completedDemos: DemoProgressDto[];
}

export function DemoCategory(props: DemoCategoryProps) {
    const { completedDemos, category } = props;
    const { title, slug, demos } = category;

    const demosWithProgress = demos.map(d => {
        const completedDemo = completedDemos && completedDemos.find(c => c.demoSlug === d.slug);
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