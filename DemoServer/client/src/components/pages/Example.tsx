import * as React from "react";
import { Page } from "../Layout";
import { ExampleService } from "../../utils/Services";
import { Row, Col } from "react-bootstrap";

interface ExamplePageProps {
}

interface ExamplePageState {
    text: string;
}

export class ExamplePage extends React.Component<ExamplePageProps, ExamplePageState> {
    private readonly service = new ExampleService();

    constructor(props) {
        super(props);

        this.state = { text: "" };
        this.loadData = this.loadData.bind(this);
    }

    componentDidMount() {
        this.loadData();
    }

    loadData() {
        this.service.getData()
            .then(result => {
                this.setState({ text: result && result.text });
            })
            .catch(error => {
                alert(error);
            });
    }

    render() {
        const { text } = this.state;

        return <Page header="Example" headerGlyph="pencil">
          <h4>Example Page</h4>
          <Row>
              <Col sm={2}>
                {text}
              </Col>
          </Row>
        </Page>;
    }
}