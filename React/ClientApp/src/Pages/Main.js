import React, { Component } from 'react';
import UserIcon from '@material-ui/icons/Group';
import { Admin, Resource } from 'react-admin';

import authProvider from './components/authProvider';
import dataProvider from './components/dataProvider';

import { SchedulerList } from './components/SchedulerList';

//controlers: login, grid, edit node, filter, order by

export class Main extends Component {

    render() {

        return (

            <Admin dataProvider={dataProvider} authProvider={authProvider} >

                <Resource name="schedulers" icon={UserIcon} list={SchedulerList} />

            </Admin>

        );
    }

}