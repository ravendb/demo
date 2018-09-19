import * as React from "react";
import { Col, Grid, Row, Glyphicon } from "react-bootstrap";
import { NavMenu } from "./ui/NavMenu";
import { LinkContainer } from "react-router-bootstrap";

export class Layout extends React.Component {
    displayName = Layout.name

    render() {
        return (
            <Grid fluid>
                <Row>
                    <Col sm={4}>
                        <NavMenu />
                    </Col>
                    <Col sm={8}>
                        {this.props.children}
                    </Col>
                </Row>
            </Grid>
        );
    }
}

interface PageBodyProps {
    header: string;
    headerGlyph?: string;
}

class PageBody extends React.Component<PageBodyProps, {}> {
    constructor(props) {
        super(props);
    }

    render() {
        const { header, headerGlyph, children } = this.props;
        const headerGlyphItem = headerGlyph && <Glyphicon glyph={headerGlyph} className="header-glyph" />;

        return <>
                <Row>
                    <Col sm={9}>
                        <h1 className="uppercase">{headerGlyphItem}{header}</h1>
                    </Col>
                </Row>
                <Row>
                    <Col className="page-body">{children}</Col>
                </Row>
            </>;
    }
}

export function PageControlBar(props) {
    return (
        <Row className="control-bar">
            {props.children}
        </Row>
    );
}

interface PageProps extends PageBodyProps {
}

export class Page extends React.Component<PageProps, {}> {
    render() {
        return <Layout><PageBody {...this.props}/></Layout>;
    }
}