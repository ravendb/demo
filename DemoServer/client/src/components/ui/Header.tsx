import * as React from "react";
import * as bsn from "bootstrap.native/dist/bootstrap-native-v4";

export class Header extends React.Component<{}, {}> {
    dropdown: HTMLElement;

    componentDidMount() {
        this.dropdown = document.getElementById("settings-dropdown");
        this.dropdown && bsn.Dropdown(this.dropdown);
    }

    componentWillUnmount() {
        this.dropdown && bsn.Dropdown(this.dropdown, "dispose");
    }

    render() {
        return <div className="header">
            <div>
                <a href="/" className="logo">
                    <img src="../img/logo.svg" alt="RavenDB Demo" />
                </a>
            </div>
            <div className="flex-grow"></div>
            <div className="progress"></div>
            <div id="settings-dropdown" className="dropdown">
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
}