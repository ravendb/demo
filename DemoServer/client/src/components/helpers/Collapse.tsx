import * as React from "react";
import classNames from "classnames";
import { expand, collapse } from "../../utils/collapse";

interface CollapseProps {
    id: string;
    show: boolean;
    className?: string;
}

export class Collapse extends React.Component<CollapseProps, {}> {
    element: HTMLElement;

    componentDidMount() {
        const { id, show } = this.props;
        this.element = document.getElementById(id);
        this.toggle(show);
    }

    toggle(show: boolean) {
        if (show) {
            expand(this.element);
        } else {
            collapse(this.element);
        }
    }

    componentDidUpdate(prevProps: CollapseProps) {
        const { show } = this.props;
        if (prevProps.show !== show) {
            this.toggle(show);
        }
    }

    render() {
        const { id, children, className } = this.props;
        const effectiveClassName = classNames("collapse", className);
            
        return <div id={id} className={effectiveClassName}>
            {children}
        </div>;
    }
}
