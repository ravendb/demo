import * as React from "react";
import * as classNames from "classnames";
import { IconLeft, IconRight, IconCancel } from "../../helpers/icons";

interface NavButtonProps {
    url: string;
}

interface WithLinkProps {
    btnClass: string;
}

class NavButton extends React.Component<NavButtonProps & WithLinkProps> {
    render() {
        const { url, btnClass, children } = this.props;

        const className = classNames("walkthrough-nav", {
            "disabled": !url
        }, btnClass);

        return <a href={url || null} role="button" className={className}>
            {children}
        </a>;
    }
}

export const PreviousButton = (props: NavButtonProps) => {
    return <NavButton {...props} btnClass="nav-prev">
        <IconLeft /> Previous
    </NavButton>;
};

export const NextButton = (props: NavButtonProps) => {
    return <NavButton {...props} btnClass="nav-next">
        Next <IconRight />
    </NavButton>;
};

export const CloseButton = (props: NavButtonProps) => {
    return <NavButton {...props} btnClass="nav-close">
        Close walkthrough <IconCancel />
    </NavButton>;
};