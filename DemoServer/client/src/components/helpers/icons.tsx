import * as React from "react";

interface IconProps {
    name: IconName;
}

export const Icon = (props: IconProps) => {
    const { name } = props;
    if (!name) {
        return null;
    }

    const className = `icon-${name}`;
    return <i className={className}></i>;
}

export type IconName = "left" | "right" | "cancel" | "settings";

export const IconLeft = () => <Icon name="left" />;
export const IconRight = () => <Icon name="right" />;
export const IconCancel = () => <Icon name="cancel" />;
export const IconSettings = () => <Icon name="settings" />;