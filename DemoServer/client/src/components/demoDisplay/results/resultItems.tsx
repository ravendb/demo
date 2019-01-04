import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { capitalizeFirstLetter } from "../../../utils/miscUtils";

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

interface ResultTableStateProps {
    results: any[];
}

interface ResultTableOwnProps {
    fields: string[];
}

type ResultTableProps = ResultTableStateProps & ResultTableOwnProps;

class ResultTableDisplay extends React.Component<ResultTableProps, {}> {
    columnHeader = (field: string) => <th key={field}>{capitalizeFirstLetter(field)}</th>;
    cell = (text: string, index: number) => <td key={`${index}_${text}`}>{text}</td>;

    row = (result: any, index: number) => {
        const { fields: columns } = this.props;

        return <tr key={index}>
            {columns.map((x, i) => this.cell(result[x], i))}
        </tr>;
    }

    render() {
        const { fields, results } = this.props;

        return <div className="results-table">
            <table>
                <thead>
                    <tr>
                        {fields && fields.map(this.columnHeader)}
                    </tr>
                </thead>
                <tbody>
                    {results && results.map((x, i) => this.row(x, i))}
                </tbody>
            </table>
        </div>;
    }
}

export const ResultTable = connect<ResultTableStateProps, {}, ResultTableOwnProps>(
    ({ demos }: AppState): ResultTableStateProps => {
        const { runResults } = demos;

        if (!runResults) {
            return {
                results: []
            };
        }

        if (runResults instanceof Array) {
            return {
                results: runResults
            };
        }

        return {
            results: [ runResults ]
        };
    }
)(ResultTableDisplay);