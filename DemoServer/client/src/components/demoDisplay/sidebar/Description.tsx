import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { Markdown } from "../../helpers/Markdown";

interface Props {
    descriptionHtml: string;
}

function DescriptionComponent(props: Props) {
    const { descriptionHtml } = props;
    return <>
        <h2>Description</h2>
        <hr />
        <Markdown>{descriptionHtml}</Markdown>
    </>;
}

export const Description = connect<Props>(
    ({ demos }: AppState): Props => {
        const { demo } = demos;
        return {
            descriptionHtml: demo && demo.descriptionHtml
        };
    }
)(DescriptionComponent);
