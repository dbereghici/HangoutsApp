import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
// import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import './Friends.css'
import AuthService from '../../services/AuthService';

const logo = require('../../images/user.png');

export class ReceivedReqFriend extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.accept = this.accept.bind(this);
        this.decline = this.decline.bind(this);

        this.state = {
            UserData: this.props.UserData
        }
    }

    accept() {
        this.state.UserData;
        this.props.UserData;
        let id1 = this.state.UserData.id;
        let id2 = JSON.parse(AuthService.getUserData()).id;
        this.props.accept(id1, id2);
    }

    decline() {
        this.state.UserData;
        this.props.UserData;
        let id1 = this.state.UserData.id;
        let id2 = JSON.parse(AuthService.getUserData()).id;
        this.props.decline(id1, id2);
    }

    render() {
        return (
            <div className="panel panel-primary">
                <div className="panel-heading">

                    <img src={logo} width="50px" height="50px" />
                    <b>{this.state.UserData.username}</b>
                </div>
                <div className="panel-body">
                    <b>
                        {this.state.UserData.firstName} {this.state.UserData.lastName}
                    </b>
                    <br />
                    {this.state.UserData.age} years
                    <br />
                    {this.state.UserData.address}
                    <br />
                    <button className="btn btn-warning glyphicon glyphicon-ok" onClick={this.accept}> Accept </button>
                    <button className="btn btn-warning glyphicon glyphicon-remove" onClick={this.decline}> Decline </button>
                </div>
            </div>
        );
    }
}
