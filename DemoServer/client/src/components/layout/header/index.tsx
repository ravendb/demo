import * as React from "react";
import * as bsn from "bootstrap.native/dist/bootstrap-native-v4";
import { IconSettings } from "../../helpers/icons";
import { MainPageDropdownItems } from "./MainPageDropdownItems";
import { DemoPageDropdownItems } from "./DemoPageDropdownItems";

export class Header extends React.Component<{}, {}> {
    dropdown: HTMLElement;

    componentDidMount() {
        this.dropdown = document.getElementById("settings-dropdown");
        this.dropdown && bsn.Dropdown(this.dropdown);
    }

    componentWillUnmount() {
        this.dropdown && bsn.Dropdown(this.dropdown, "dispose");
    }

    isOnMainPage() {
        return window.location.pathname === "/";
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
                    <IconSettings />
                </a>
                <div className="dropdown-menu placement-right">
                    {this.isOnMainPage()
                        ? <MainPageDropdownItems />
                        : <DemoPageDropdownItems />
                    }
                </div>
            </div>
        </div>;
    }
}