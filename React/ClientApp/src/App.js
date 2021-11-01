import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './Pages/components/Layout';
import { Main } from './Pages/Main';

import './custom.css'

export default class App extends Component {

  static displayName = App.name;

  render () {
    return (
        <Layout>
            <Route exact path='/' component={Main} />
        </Layout>
    );
  }
}
