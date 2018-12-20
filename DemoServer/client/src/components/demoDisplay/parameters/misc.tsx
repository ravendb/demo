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

export const FileUploadWarning = () => (
    <div className="bg-warning padding padding-sm">
        <i className="icon-warning margin-right margin-xs"></i>
        Caution: uploaded files will be stored in a public database.
    </div>
);