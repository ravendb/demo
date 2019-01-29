import * as React from "react";
import { IconCancel, IconReload } from "../../helpers/icons";

interface DialogProps {
    show: boolean;
}

export class Dialog extends React.Component<DialogProps, {}> {
    public render() {
        const { show, children } = this.props;

        return show && <div id="dialog">
            <div className="message">
                {children}
            </div>
            <div className="overlay"></div>
        </div>;
    }
}

export class ButtonPanel extends React.Component<{}, {}> {
    public render() {
        return <div className="text-center margin-top">
            <div className="btn-group">
                {this.props.children}
            </div>
        </div>;
    }
}

interface DialogButtonProps {
    onClick: () => void;
    text?: string;
    disabled?: boolean;
}

export const CancelButton = (props: DialogButtonProps) => {
    const { text, onClick, disabled } = props;

    return <button className="btn btn-default" onClick={onClick} disabled={disabled}>
        <IconCancel /> {text || "Cancel"}
    </button>;
};

export const AcceptButton = (props: DialogButtonProps) => {
    const { text, onClick, disabled } = props;

    return <button className="btn btn-danger" onClick={onClick} disabled={disabled}>
        <IconReload /> {text || "Yes"}
    </button>;
};

export const ReloadButton = (props: DialogButtonProps) => {
    const { text, onClick, disabled } = props;

    return <button className="btn btn-danger" onClick={onClick} disabled={disabled}>
        <IconReload /> {text || "Reload"}
    </button>;
};
