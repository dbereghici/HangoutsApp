import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
// import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import './Friends.css'
import AuthService from '../../services/AuthService';

const logo = require('../../images/user.png');

export class SendReqFriend extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.cancel = this.cancel.bind(this);

        this.state = {
            UserData: this.props.UserData
        }
    }

    cancel() {
        this.state.UserData;
        this.props.UserData;
        let id1 = this.state.UserData.id;
        let id2 = JSON.parse(AuthService.getUserData()).id;
        this.props.cancel(id1, id2);
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
                    <button className="btn btn-warning glyphicon glyphicon-remove" onClick={this.cancel}> Cancel </button>
                </div>
            </div>
        );
    }
}
