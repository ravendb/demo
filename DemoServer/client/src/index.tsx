import "../styles/styles.scss";

window["Prism"] = { manual: true } as any;
import Prism from "./libs/prism.bundle";
console.log(Prism);

import * as React from "react";
import * as ReactDOM from "react-dom";

import { ConnectedRouter } from "connected-react-router";
import { HelmetProvider } from "react-helmet-async";
import App from "./App";
import { Provider } from "react-redux";
import { store, history } from "./store";

const rootElement = document.getElementById('root');

ReactDOM.render(
  <HelmetProvider>
    <Provider store={store}>
      <ConnectedRouter history={history}>
        <App />
      </ConnectedRouter>
    </Provider>
  </HelmetProvider>,
  rootElement);

import 'bootstrap.native/dist/bootstrap-native-v4';
