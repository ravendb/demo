import * as React from "react";
import * as classNames from "classnames";

interface ToastDisplayProps {
    show: boolean;
    className?: string;
}

class ToastDisplay extends React.Component<ToastDisplayProps, {}> {
    render() {
        const { show, children, className } = this.props;

        const effectiveClassName = classNames("toast", {
            "active": show
        }, className);

        return <div className={effectiveClassName}>{children}</div>;
    }
}

const defaultDisplayDuration = 5000;

export interface ToastStateProps {
    show: boolean;
}

export interface ToastDispatchProps {
    hideToast: () => void;
}

export interface ToastOwnProps {
    displayDuration?: number;
    className?: string;
}

export type ToastProps = ToastStateProps & ToastDispatchProps & ToastOwnProps;

export class Toast extends React.Component<ToastProps, {}> {
    private timerId: any;

    constructor(props) {
        super(props);
        this.fadeOut = this.fadeOut.bind(this);
    }

    componentDidUpdate(prevProps: ToastProps) {
        const { show } = this.props;
        if (show == prevProps.show) {
            return;
        }

        if (show) {
            this.fadeOut();
        }
    }

    componentWillUnmount() {
        if (this.timerId) {
            clearTimeout(this.timerId);
        }
    }

    fadeOut() {
        const { displayDuration } = this.props;
        const duration = displayDuration || defaultDisplayDuration;

        this.timerId = setTimeout(() => {
            if (this.props) {
                this.props.hideToast();
            }
        }, duration);
    }

    render() {
        return <ToastDisplay {...this.props}>{this.props.children}</ToastDisplay>;
    }
}