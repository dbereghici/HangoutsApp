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

        this.changeBackgroundColor = this.changeBackgroundColor.bind(this);
        this.sendRequest = this.sendRequest.bind(this);
        this.deleteRequest = this.deleteRequest.bind(this);
        this.acceptRequest = this.acceptRequest.bind(this);
        this.deleteGroup = this.deleteGroup.bind(this);
        this.toGroup = this.toGroup.bind(this);

        this.state = {
            GroupData: this.props.GroupData,
            changeColor: false,
            redirectToGroup: false
        }
    }

    sendRequest() {
        this.props.sendRequest(this.state.GroupData.id)
    }

    deleteRequest() {
        this.props.deleteRequest(this.state.GroupData.id)
    }

    acceptRequest() {
        this.props.acceptRequest(this.state.GroupData.id)
    }

    deleteGroup(){
        this.props.deleteGroup(this.state.GroupData.id)
    }

    toGroup(){
        this.setState({
            redirectToGroup: true
        })
    }


    changeBackgroundColor() {
        this.setState({ changeColor: !this.state.changeColor })
    }


    render() {
        if (this.state.redirectToGroup) {
            let redirectTo = '/group/' + this.state.GroupData.id;
            return <Redirect to={redirectTo} />;
        }
        var style1 = { backgroundColor: '#110bcc32', width: "400px" };
        var style2 = { backgroundColor: 'white', width: "400px" };
        return (
            <div className="panel panel-warning  "
                style={this.state.changeColor ? style1 : style2}
                onMouseEnter={this.changeBackgroundColor}
                onMouseLeave={this.changeBackgroundColor}
            >
                <div className="panel-heading" >
                    <img src={logo} width="50px" height="50px" />
                    <b>    {this.state.GroupData.name} </b>
                </div>
                <div className="panel-body">
                    <b> Administrator: </b> {this.state.GroupData.admin}
                    <br />
                    {this.state.GroupData.nrOfMembers} members
                    <br />
                    <button className="btn btn-danger" onClick={this.toGroup}> More... </button>
                    <br /><br />
                    {
                        this.props.status ?
                            //my groups
                            this.props.status === "admin" ?
                                //admin
                                <div>
                                    <p>You are the admin of this group </p>
                                    <button
                                        className="btn btn-warning glyphicon glyphicon-remove"
                                        onClick = {this.toGroup}
                                    > Manage
                                    </button>
                                    <button
                                        className="btn btn-warning glyphicon glyphicon-remove"
                                        onClick = {this.deleteGroup}
                                    > Delete
                                    </button>
                                </div>
                                :
                                this.props.status === "received" ?
                                    //received
                                    <div>
                                        <p>You are invited to join this group</p>
                                        <button
                                            className="btn btn-warning glyphicon glyphicon-remove"
                                            onClick = {this.acceptRequest}
                                        > Accept
                                    </button>
                                        <button
                                            className="btn btn-warning glyphicon glyphicon-remove"
                                            onClick = {this.deleteRequest}
                                        > Decline
                                    </button>
                                    </div>
                                    :
                                    this.props.status === "sent" ?
                                        //sent
                                        <div>
                                            <p>You have sent a request to join this group</p>
                                            <button
                                                className="btn btn-warning glyphicon glyphicon-remove"
                                                onClick = {this.deleteRequest}
                                            > Cancel
                                            </button>
                                        </div>
                                        :
                                        <div>
                                            <p>You are member of this group</p>
                                            <button
                                                className="btn btn-warning glyphicon glyphicon-remove"
                                                onClick = {this.deleteRequest}
                                            > Leave
                                            </button>
                                        </div>
                            :
                            //others
                            <div>
                                <div>
                                    <p>Send a request to join this group</p>
                                    <button
                                        className="btn btn-warning glyphicon glyphicon-remove"
                                        onClick={this.sendRequest}
                                    > Send
                                    </button>
                                </div>
                            </div>
                    }
                    {/* <button className="btn btn-warning glyphicon glyphicon-remove" onClick = {this.addFriend}> Add friend </button> */}

                </div>
            </div>
        );
    }
}
