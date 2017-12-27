import * as React from 'react';
// import 'bootstrap/dist/css/bootstrap.css';
// import 'bootstrap/dist/css/bootstrap-theme.css';
import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import { Header } from '../Header/Header';

export class Groups extends BaseComponent {
    constructor(props: any) {
        super(props);
    
    }


    render() {
        if (!this.state.isAuth) {
            return <Redirect to='/authentication' />;
        }

        return (

            <div>
                <Header />
                Groups
            </div>
        );
    }
}
