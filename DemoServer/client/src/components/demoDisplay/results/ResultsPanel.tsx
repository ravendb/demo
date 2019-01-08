import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { Dispatch } from "redux";
import { hideResults } from "../../../store/actions/demoActions";
import { Collapse } from "../../helpers/Collapse";

interface StateProps {
    showPanel: boolean;
    loadingResults: boolean;
}

interface DispatchProps {
    hidePanel: () => void;
}

type ResultsPanelProps = StateProps & DispatchProps;

class ResultsPanelDisplay extends React.Component<ResultsPanelProps, {}> {
    render() {
        const { loadingResults, children, hidePanel, showPanel } = this.props;
        return <Collapse id="results-panel" className="results-container" show={showPanel}>
            <div className="results">
                <div className="flex-horizontal margin-bottom">
                    <div>
                        <h1>RESULTS</h1>
                    </div>
                    <div className="flex-grow"></div>
                    {/* <div className="results-speed">
                        <div>
                            <div className="text-muted">CLIENT</div>
                            <div>dummy data</div>
                        </div>
                        <i className="icon-speed"></i>
                        <div>
                            <div className="text-muted">SERVER</div>
                            <div>dummy data</div>
                        </div>
                    </div> */}
                    <div>
                        <button className="minimize-results" onClick={hidePanel}>
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
        </Collapse>;
    }
}

export const ResultsPanel = connect<StateProps, DispatchProps, {}>(
    ({ demos }: AppState) => {
        return {
            loadingResults: demos.loadingRunResults,
            showPanel: demos.showResultsPanel
        };
    },
    (dispatch: Dispatch): DispatchProps => {
        return {
            hidePanel: () => dispatch(hideResults())
        };
    }
)(ResultsPanelDisplay);