import * as React from "react";

interface ControlsProps {
    toggleCollapse: () => void;
}

export function Controls(props: ControlsProps) {
    const { toggleCollapse } = props;

    return <div className="sidebar-controlls">
        <button id="collapseSidebar" className="collapse-sidebar" onClick={toggleCollapse}>
            <i className="icon-collapse"></i>
            <i className="icon-expand"></i>
        </button>
        <a href="/" className="back-button"><i className="icon-left"></i></a>
    </div>;
}