import * as React from "react";
import * as classNames from "classnames";

interface SpinnerProps {
    show: boolean;
}

interface SpinnerState {
    isDisplayed: boolean;
}

export class Spinner extends React.Component<SpinnerProps, SpinnerState> {
    private timerId: any;

    constructor(props) {
        super(props);

        this.state = {
            isDisplayed: true
        };

        this.fadeIn = this.fadeIn.bind(this);
        this.fadeOut = this.fadeOut.bind(this);
    }

    componentDidMount() {
        const { show } = this.props;
        if (show) {
            this.fadeIn();
        }
    }

    componentDidUpdate(prevProps: SpinnerProps) {
        const { show } = this.props;
        if (show == prevProps.show) {
            return;
        }

        if (show) {
            this.fadeIn();
        } else {
            this.fadeOut();
        }
    }

    componentWillUnmount() {
        if (this.timerId) {
            clearTimeout(this.timerId);
        }
    }

    fadeIn() {
        this.timerId = setTimeout(() => {
            this.setState({ isDisplayed: true })
        }, 2);
    }

    fadeOut() {
        this.timerId = setTimeout(() => {
            this.setState({ isDisplayed: false })
        }, 300);
    }

    getClassName() {
        const { show } = this.props;
        const { isDisplayed } = this.state;

        const isHidden = !show && !isDisplayed;

        if (isHidden) {
            return "hidden";
        }

        const isFadingIn = show && !isDisplayed;
        const isFadingOut = !show && isDisplayed;

        if (isFadingIn || isFadingOut) {
            return "";
        }

        return "active";
    }

    render() {
        const className = classNames("spinner", this.getClassName());
        return <div className={className}></div>;
    }
}