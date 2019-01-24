import * as React from "react";
import * as classNames from "classnames";
import { DemoWithProgress } from "../../models/demo";
import { connect } from "react-redux";
import { Dispatch } from "redux";
import { goToDemoPage } from "../../store/actions/navigationActions";

interface DispatchProps {
    goToDemoPage: () => void;
}

interface OwnProps {
    category: string;
    demo: DemoWithProgress;
}

type Props = DispatchProps & OwnProps;

function DemoItemComponent(props: Props) {
    const { demo, goToDemoPage } = props;

    const className = classNames("demo-item", {
        "done": demo.completed
    });

    return <a className={className} onClick={goToDemoPage} >
        <img src="../img/demo-item.png" />
        <div className="title">{demo.title}</div>
    </a>;
}

function mapDispatchToProps(dispatch: Dispatch, ownProps: OwnProps): DispatchProps {
    const { demo, category } = ownProps;

    return {
        goToDemoPage: () => dispatch(goToDemoPage(category, demo.slug))
    };
}

export const DemoItem = connect<{}, DispatchProps, OwnProps>(
    () => ({}),
    mapDispatchToProps
)(DemoItemComponent);