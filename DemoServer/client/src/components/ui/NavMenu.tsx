import * as React from "react";
import { Link } from "react-router-dom";
import { Glyphicon, Nav, Navbar, NavItem } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";

interface NavbarSectionProps {
  title: string;
}

export class NavbarSection extends React.Component<NavbarSectionProps, {}> {
  constructor(props) {
    super(props);
  }

  render() {
    const { title, children } = this.props;

    return (
      <>
        <NavbarDivider>{title}</NavbarDivider>
        {children}
      </>
    );
  }
}

function NavbarDivider(props) {
  return (
    <NavItem className="navbar-divider">
      <hr />
      <h4>{props.children}</h4>
    </NavItem>
  );
}

export class NavMenu extends React.Component<{}, {}> {
  displayName = NavMenu.name

  render() {
    return (
      <Navbar fixedTop fluid>
        <Navbar.Header>
          <Navbar.Brand>
            <Link to={'/'}>Customers</Link>
          </Navbar.Brand>
          <Navbar.Toggle />
        </Navbar.Header>
        <Nav>
          <LinkContainer to={'/'} exact>
            <NavItem>
              <Glyphicon glyph='home' /> Home
            </NavItem>
          </LinkContainer>
          <LinkContainer to={'/example'}>
            <NavItem>
              <Glyphicon glyph='check' /> Example Page
            </NavItem>
          </LinkContainer>
        </Nav>
      </Navbar>
    );
  }
}
