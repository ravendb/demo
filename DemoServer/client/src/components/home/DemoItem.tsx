import * as React from "react";
import classNames from "classnames";
import { DemoWithProgress } from "../../models/demo";
import { connect } from "react-redux";
import { goToDemoPage } from "../../store/actions/navigation";
import { CategorySlug } from "../../models/slugs";
import { DemoThunkDispatch } from "../../store";
import { getDemoImageSrc } from "../../store/selectors/demos";
import { Link } from "react-router-dom";
import { createDemoWithWalkthroughPath } from "../../utils/paths";

interface DispatchProps {
    goToDemoPage: () => void;
}

interface OwnProps {
    category: CategorySlug;
    demo: DemoWithProgress;
}

type Props = DispatchProps & OwnProps;

function DemoItemComponent(props: Props) {
    const { category, demo, goToDemoPage } = props;

    const className = classNames("demo-item", {
        "done": demo.completed
    });

    const imageSrc = getDemoImageSrc(category, demo.slug);

    const url = createDemoWithWalkthroughPath({
        category: category,
        demo: demo.slug
    });

    return <Link to={url} className={className} onClick={goToDemoPage} >
        <div className="bkg"><img src={imageSrc} /></div>
        <div className="title">{demo.title}</div>
    </Link>;
}

function mapDispatchToProps(dispatch: DemoThunkDispatch, ownProps: OwnProps): DispatchProps {
    const { demo, category } = ownProps;

    return {
        goToDemoPage: () => dispatch(goToDemoPage(category, demo.slug, false))
    };
}

export const DemoItem = connect<{}, DispatchProps, OwnProps>(
    () => ({}),
    mapDispatchToProps
)(DemoItemComponent);
