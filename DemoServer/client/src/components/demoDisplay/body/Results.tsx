import * as React from "react";

interface ResultsProps {
    elementId: string;
    clientExecTime: string;
    serverExecTime: string;
}

export class Results extends React.Component<ResultsProps, {}> {
    render() {
        const { elementId, clientExecTime, serverExecTime } = this.props;
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
                            <div>{clientExecTime}</div>
                        </div>
                        <i className="icon-speed"></i>
                        <div>
                            <div className="text-muted">SERVER</div>
                            <div>{serverExecTime}</div>
                        </div>
                    </div>
                    <div>
                        <button className="minimize-results" data-toggle="collapse" data-target={`#${elementId}`} type="button">
                            <i className="icon-down"></i>
                        </button>
                    </div>
                </div>

                <div className="text-center">
                    <img src="../img/file-added.png" />
                    <h2>File succesfully added</h2>
                </div>
            </div>
        </div>;
    }
}