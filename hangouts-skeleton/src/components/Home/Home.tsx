import * as React from 'react'
import BaseComponent from '../BaseComponent/BaseComponent';
import { Redirect } from 'react-router';
import { Header } from '../Header/Header';
import AuthService from '../../services/AuthService';

export default class Home extends BaseComponent{
    constructor(props : any){
        super(props);
        // let auth = this.isAuth;
        // console.log(auth);
        // debugger;
    }



    render(){
        let auth = this.state.isAuth;
        if (!auth){
            return <Redirect to='/authentication'/>;
        }
        return (
            <div>
                {/* <h1> Home Page </h1> */}
                <Header />
                <h2> Hello {JSON.parse(AuthService.getUserData()).firstName}</h2>
            </div>
        );
    }
}