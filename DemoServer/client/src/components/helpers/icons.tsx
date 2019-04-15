import * as React from "react";

interface IconProps {
    name: string;
}

export const Icon = (props: IconProps) => {
    const { name } = props;
    if (!name) {
        return null;
    }

    return <i className={`icon-${name}`}></i>;
};

export const IconLeft = () => <Icon name="left" />;
export const IconRight = () => <Icon name="right" />;
export const IconCancel = () => <Icon name="cancel" />;
export const IconSettings = () => <Icon name="settings" />;
export const IconLearn = () => <Icon name="learn" />;
export const IconPlay = () => <Icon name="play" />;
export const IconStudio = () => <Icon name="studio" />;
export const IconReload = () => <Icon name="reload" />;
export const IconCollapse = () => <Icon name="collapse" />;
export const IconExpand = () => <Icon name="expand" />;
export const IconConfirm = () => <Icon name="confirm" />;
