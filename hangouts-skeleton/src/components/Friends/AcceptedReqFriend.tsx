import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
// import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import './Friends.css'
import AuthService from '../../services/AuthService';

export class AcceptedReqFriend extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.unfriend = this.unfriend.bind(this);

        this.state = {
            UserData: this.props.UserData
        }
    }

    unfriend(){
        this.state.UserData;
        this.props.UserData;
        let id1 = this.state.UserData.id;
        let id2 = JSON.parse(AuthService.getUserData()).id;
        this.props.unfriend(id1, id2);
    }

    render() {
        return (
            <div className="panel panel-primary">
                <div className="panel-heading">
                  {this.state.UserData.username}
                </div>
                <div className="panel-body">
                    <b>
                    {this.state.UserData.firstName} {this.state.UserData.lastName}
                    </b>
                    <br/>
                    {this.state.UserData.age} years
                    <br/>
                    {this.state.UserData.address}
                    <br/>
                    <button className="btn btn-warning glyphicon glyphicon-remove" onClick = {this.unfriend}> Unfriend </button>
                    <button className="btn btn-warning glyphicon glyphicon-envelope"> Message </button>
                </div>
            </div>
        );
    }
}
