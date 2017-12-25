import * as React from 'react'
import BaseComponent from '../BaseComponent/BaseComponent';
import { Redirect } from 'react-router';
import { Header } from '../Header/Header';

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
            </div>
        );
    }
}