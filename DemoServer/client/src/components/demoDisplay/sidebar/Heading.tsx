import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { Dispatch } from "redux";
import { IconLeft } from "../../helpers/icons";
import { goToMainPage } from "../../../store/actions/navigation";

interface StateProps {
    title: string;
}

interface DispatchProps {
    goToMainPage: () => void;
}

type Props = StateProps & DispatchProps;

function HeadingComponent(props: Props) {
    const { title, goToMainPage } = props;

    return <div className="sidebar-heading">
        <a onClick={goToMainPage} className="back-button">
            <IconLeft />
        </a>
        <h1>{title}</h1>
    </div>;
}

function mapStateToProps({ demos }: AppState): StateProps {
    const { demo } = demos;

    return {
        title: demo && demo.title
    };
}

function mapDispatchToProps(dispatch: Dispatch): DispatchProps {
    return {
        goToMainPage: () => dispatch(goToMainPage())
    };
}

export const Heading = connect<StateProps, DispatchProps, {}>(
    mapStateToProps,
    mapDispatchToProps
)(HeadingComponent);
