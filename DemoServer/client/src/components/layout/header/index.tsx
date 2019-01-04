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
            <div>
                <select id="selectDemo">
                    <option disabled>Basics</option>
                    <option>The Document Store</option>
                    <option>The Session</option>
                    <option>Create Document</option>
                    <option>Edit Document</option>
                    <option>Delete Document</option>
                    <option disabled>Attachments</option>
                    <option>Store Attachment</option>
                    <option disabled>Revisions</option>
                    <option>Enable Revisions</option>
                    <option disabled>Queries</option>
                    <option>Query Overview</option>
                    <option>Full Collection Query</option>
                    <option>Query by Document ID</option>
                    <option>Filtering Query Results</option>
                    <option>Projecting Individual Fields</option>
                    <option>Projecting Using Functions</option>
                    <option disabled>Static Indexes</option>
                    <option>Static Indexes Overview</option>
                    <option>Map Index</option>
                    <option>Map Reduce Index</option>
                    <option disabled>Text Search</option>
                    <option>Full Text Search with Static Index</option>
                    <option disabled>Advanced</option>
                    <option>Create Database</option>
                </select>
            </div>
            <div className="flex-grow"></div>
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