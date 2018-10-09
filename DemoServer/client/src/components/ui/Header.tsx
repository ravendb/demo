import * as React from "react";

interface HeaderProps {
}

export function Header(props: HeaderProps) {
    return <div className="header">
        <div>
            <a href="/" className="logo">
                <img src="../img/logo.svg" alt="RavenDB Demo" />
            </a>
        </div>
        <div className="flex-grow"></div>
        <div className="progress"></div>
        <div className="dropdown">

            <a className="settings" data-toggle="dropdown">
                <i className="icon-settings"></i>
            </a>
            <div className="dropdown-menu placement-right">
                <a className="dropdown-item" href="#">Reset demo</a>
                <a className="dropdown-item" href="#">Share</a>
            </div>
        </div>

    </div>;
}