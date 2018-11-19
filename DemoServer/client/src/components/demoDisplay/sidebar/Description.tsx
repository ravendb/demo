import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { RawHtml } from "../../helpers/RawHtml";

interface Props {
    descriptionHtml: string;
}

function DescriptionComponent(props: Props) {
    const { descriptionHtml } = props;
    return <>
        <h2>Description</h2>
        <hr />
        <RawHtml>{descriptionHtml}</RawHtml>
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