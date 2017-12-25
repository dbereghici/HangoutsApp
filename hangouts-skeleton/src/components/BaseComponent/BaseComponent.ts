import * as React from 'react';
import AuthService from '../../services/AuthService'

export default class BaseComponent extends React.Component<any, any> {
    // protected isAuth : boolean = false;
    
    constructor(props : any){
        super(props);
        this.state = {
            isAuth : AuthService.isAuth()
        }
    }

    checkAuth(){
        this.setState({isAuth : AuthService.isAuth()})
    }
}