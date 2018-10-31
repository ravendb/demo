import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";

interface OwnProps {
    elementId: string;
}

interface StateProps {
    loadingResults: boolean;
}

type ResultsPanelProps = OwnProps & StateProps;

class ResultsPanelDisplay extends React.Component<ResultsPanelProps, {}> {
    render() {
        const { elementId, loadingResults, children } = this.props;
        return <div id={elementId} className="results-container collapse">
            <div className="results">
                <div className="flex-horizontal">
                    <div>
                        <h1>RESULTS</h1>
                    </div>
                    <div className="flex-grow"></div>
                    <div className="results-speed">
                        <div>
                            <div className="text-muted">CLIENT</div>
                            <div>dummy data</div>
                        </div>
                        <i className="icon-speed"></i>
                        <div>
                            <div className="text-muted">SERVER</div>
                            <div>dummy data</div>
                        </div>
                    </div>
                    <div>
                        <button className="minimize-results" data-toggle="collapse" data-target={`#${elementId}`} type="button">
                            <i className="icon-down"></i>
                        </button>
                    </div>
                </div>

                <div className="text-center">
                    {loadingResults
                        ? "LOADING RESULTS..."
                        : children}
                </div>
            </div>
        </div>;
    }
}

export const ResultsPanel = connect<StateProps, {}, OwnProps>(
    ({ demos }: AppState) => {
        return {
            loadingResults: demos.loadingRunResults
        };
    }
)(ResultsPanelDisplay);