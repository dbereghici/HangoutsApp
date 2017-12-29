import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
// import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import { Redirect } from 'react-router';

const logo = require('../../images/group.png');

export class GroupPanel extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.onClickHandler = this.onClickHandler.bind(this);
        this.changeBackgroundColor = this.changeBackgroundColor.bind(this);
        // this.addFriend = this.addFriend.bind(this);

        this.state = {
            GroupData: this.props.GroupData,
            changeColor: false,
            redirectToGroup: false
        }
    }

    // addFriend(){
    //     this.state.UserData;
    //     this.props.UserData;
    //     let id1 = this.state.UserData.id;
    //     let id2 = JSON.parse(AuthService.getUserData()).id;
    //     this.props.addFriend(id1, id2);
    // }

    onClickHandler(){
        this.setState({
            redirectToGroup : true
        })
    }

    changeBackgroundColor(){
        this.setState({changeColor : !this.state.changeColor})
    }

    render() {
        if (this.state.redirectToGroup) {
            let redirectTo = '/group/' + this.state.GroupData.id;
            return <Redirect to={redirectTo} />;
        }
        var style1 = {backgroundColor: '#110bcc32', width: "400px"};
        var style2 = {backgroundColor:'white', width: "400px"};
        return (
            <div className="panel panel-danger " 
                style={this.state.changeColor ? style1 : style2} 
                onClick={this.onClickHandler} 
                onMouseEnter={this.changeBackgroundColor} 
                onMouseLeave={this.changeBackgroundColor}
            >
                <div className="panel-heading">
                <img src={logo} width="50px" height="50px" />
                <b>    {this.state.GroupData.name} </b>
                </div>
                <div className="panel-body">
                    <b> Administrator: </b> {this.state.GroupData.admin} 
                    <br/>
                    {this.state.GroupData.nrOfMembers} members   
                    <br/><br/> 
                    <p> Click for more information... </p>               
                    {/* <button className="btn btn-warning glyphicon glyphicon-remove" onClick = {this.addFriend}> Add friend </button> */}
                </div>
            </div>
        );
    }
}
