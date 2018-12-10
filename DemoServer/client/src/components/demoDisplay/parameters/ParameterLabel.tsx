import * as React from "react";

export interface ParameterLabelProps {
    name: string;
}

export function ParameterLabel(props: ParameterLabelProps) {
    const { name } = props;

    return <div className="parameter-label">
        <div className="name">{name}</div>
        <div className="type"></div>
    </div>;
}