import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
// import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import './Friends.css'
import AuthService from '../../services/AuthService';
import { Redirect } from 'react-router';

const logo = require('../../images/user.png');

export class AcceptedReqFriend extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.unfriend = this.unfriend.bind(this);
        this.message = this.message.bind(this);
        this.changeBackgroundColor = this.changeBackgroundColor.bind(this);
        this.onClickHandler = this.onClickHandler.bind(this);

        this.state = {
            UserData: this.props.UserData,
            redirectToChat: false,
            changeColor: false
        }
    }

    unfriend() {
        let id1 = this.state.UserData.id;
        let id2 = JSON.parse(AuthService.getUserData()).id;
        this.props.unfriend(id1, id2);
    }

    message() {
        this.setState({
            redirectToChat: true
        })
    }

    changeBackgroundColor(){
        this.setState({changeColor : !this.state.changeColor})
    }

    onClickHandler(){
        if(this.props.onClickHandler)
            this.props.onClickHandler(this.state.UserData.id, this.state.UserData.firstName + " " + this.state.UserData.lastName);
    }

    render() {
        if (this.state.redirectToChat) {
            let redirectTo = '/chat/user/' + this.state.UserData.id;
            return <Redirect to={redirectTo} />;
        }
        var style1 = {backgroundColor: '#110bcc32'};
        var style2 = {backgroundColor:'white'};
        return (
            <div className="panel panel-primary " 
                style={this.state.changeColor ? style1 : style2} 
                onClick={this.onClickHandler} 
                onMouseEnter={this.changeBackgroundColor} 
                onMouseLeave={this.changeBackgroundColor}
            >
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
                    {this.props.displayButtons ? 
                    <div/>
                    :
                    <div>
                    <button className="btn btn-warning glyphicon glyphicon-remove" onClick={this.unfriend} > Unfriend </button>
                    <button className="btn btn-warning glyphicon glyphicon-envelope" onClick={this.message} > Message </button>
                    </div>
                    
                    }
                </div>
            </div>
        );
    }
}
