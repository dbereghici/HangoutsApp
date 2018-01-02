import * as React from 'react'
import BaseComponent from '../BaseComponent/BaseComponent';
import { Redirect } from 'react-router';
import { Header } from '../Header/Header';
import AuthService from '../../services/AuthService';
import MyPlansList from '../Plan/MyPlansList';

export default class Home extends BaseComponent {
    constructor(props: any) {
        super(props);

    }



    render() {
        let auth = this.state.isAuth;
        if (!auth) {
            return <Redirect to='/authentication' />;
        }
        return (
            <div >
                <Header />
                <div className="demoForm">
                    <h1 style={{ color: "#08a336" }}> Hello {JSON.parse(AuthService.getUserData()).firstName} !</h1> 
                </div>
                <MyPlansList />
            </div>
        );
    }
}