import * as React from "react";

interface ToastDisplayProps {
    show: boolean;
}

class ToastDisplay extends React.Component<ToastDisplayProps, {}> {
    render() {
        const { show, children } = this.props;
        const className = show
            ? "toast active"
            : "toast";

        return <div className={className}>{children}</div>;
    }
}

const displayDuration = 3500;

export interface ToastStateProps {
    show: boolean;
}

export interface ToastDispatchProps {
    hideToast: () => void;
}

export type ToastProps = ToastStateProps & ToastDispatchProps;

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
        this.timerId = setTimeout(() => {
            if (this.props) {
                this.props.hideToast();
            }
        }, displayDuration);
    }

    render() {
        return <ToastDisplay {...this.props}>{this.props.children}</ToastDisplay>;
    }
}