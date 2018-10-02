import * as React from "react";
import { Row, Col } from "react-bootstrap";

interface ExampleDisplayProps {
    loading: boolean;
    text: string;
    handleDataLoad: () => void;
}

export class ExampleDisplay extends React.Component<ExampleDisplayProps, {}> {
    componentDidMount() {
        this.props.handleDataLoad();
    }

    render() {
        const { loading, text } = this.props;
        const body = loading ? "Loading Result..." : `Result: ${text}`;

        return <>
          <h4>Example Page</h4>
          <Row>
              <Col sm={2}>
                {body}
              </Col>
          </Row>
        </>;
    }
}