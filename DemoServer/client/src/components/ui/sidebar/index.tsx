import * as React from "react";
import { Controls } from "./Controls";
import { Heading } from "./Heading";
import { LanguageSelect } from "./LanguageSelect";
import { Description } from "./Description";
import { Walkthrough } from "./Walkthrough";
import { Assets } from "./Assets";

function Body() {
    return <div className="sidebar-body">
        <Heading text="Lorem ipsum dolor sit amet" />
        <LanguageSelect />
        <Description title="Description">
            <p>
                Lorem ipsum dolor sit amet, <strong>consectetuer adipiscing</strong> elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat.
                </p>
            <p>
                Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi.
                </p>
        </Description>
        <Walkthrough items={[
            { href: "#", title: "Lorem ipsum dolor sit amet" },
            { href: "#", title: "Cons ectetuer adipiscing elit" },
            { href: "#", title: "Sed diam nonummy nibh euismod tincidunt" },
            { href: "#", title: "Ut laoreet dolore magna aliquam erat volutpat" },
            { href: "#", title: "Ut wisi enim ad minim veniam, quis nostrud exerci tation" },
            { href: "#", title: "Ullamcorper suscipit lobortis nisl ut aliquip ex ea" }
        ]} />
        <Assets items={[
            { icon: "link", href: "#", title: "Lorem ipsum dolor sit amet" },
            { icon: "download", href: "#", title: "consectetuer adipiscing elit" },
            { icon: "document", href: "#", title: "sed diam nonummy nibh euismod tincidunt" },
            { icon: "link", href: "#", title: "ut laoreet dolore magna" },
            { icon: "link", href: "#", title: "aliquam erat volutpat" },
            { icon: "link", href: "#", title: "augue duis dolore" }
        ]} />
    </div>;
}

interface SidebarProps {
}

interface SidebarState {
    sidebarCollapsed: boolean;
}

export class Sidebar extends React.Component<SidebarProps, SidebarState> {
    constructor(props) {
        super(props);

        this.state = {
            sidebarCollapsed: false
        };

        this.handleToggleCollapse = this.handleToggleCollapse.bind(this);
    }

    handleToggleCollapse() {
        this.setState({
            sidebarCollapsed: !this.state.sidebarCollapsed
        });
    }

    render() {
        const { sidebarCollapsed } = this.state;
        const customClass = sidebarCollapsed ? "small" : "";

        return <div id="sidebar" className={`sidebar ${customClass}`}>
            <Controls toggleCollapse={this.handleToggleCollapse} />
            <Body />
        </div>;
    }
}