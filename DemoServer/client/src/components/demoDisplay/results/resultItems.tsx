import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";

export const DocumentCreated = () => (
    <div className="text-center">
        <img src="../img/file-added.png" />
        <h2>Document succesfully created</h2>
    </div>
);

interface ResultTextProps {
    text: string;
}

const ResultTextDisplay = (props: ResultTextProps) => {
    const { text } = props;
    return <div className="text-center">
        <h2>{text}</h2>
    </div>;
};

export const ResultText = connect<ResultTextProps>(
    ({ demos }: AppState): ResultTextProps => {
        return {
            text: demos.runResults as string
        }
    }
)(ResultTextDisplay);