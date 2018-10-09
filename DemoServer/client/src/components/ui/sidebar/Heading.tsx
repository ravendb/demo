import * as React from "react";

interface HeadingProps {
    text: string;
}

export function Heading(props: HeadingProps) {
    const { text } = props;

    return <div className="sidebar-heading">
        <a href="/" className="back-button"><i className="icon-left"></i></a>
        <h1>{text}</h1>
    </div>;
}