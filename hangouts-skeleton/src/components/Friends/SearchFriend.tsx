import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
// import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import './Friends.css'
import AuthService from '../../services/AuthService';

const logo = require('../../images/user.png');

export class SearchFriend extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.addFriend = this.addFriend.bind(this);

        this.state = {
            UserData: this.props.UserData,
            authUser: JSON.parse(AuthService.getUserData())
        }
    }

    addFriend() {
        let id1 = this.state.UserData.id;
        let id2 = this.state.authUser.id;
        this.props.addFriend(id1, id2);
    }

    render() {
        let isMyAccount = this.state.authUser.id === this.state.UserData.id;
        return (
            <div className="panel panel-primary">
                <div className="panel-heading">
                    <img src={logo} width="50px" height="50px" />
                    <b>{this.state.UserData.username} {isMyAccount ? <p>(me)</p> : <div/>}</b>
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
                    {!isMyAccount ? 
                        <button className="btn btn-warning glyphicon glyphicon-remove" onClick={this.addFriend}> Add friend </button> 
                        : 
                        <div/>}
                </div>
            </div>
        );
    }
}
