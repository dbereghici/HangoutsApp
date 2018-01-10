import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
// import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import './Friends.css'
import FriendService from '../../services/FriendsService';
import AuthService from '../../services/AuthService';
import { SendReqFriend } from './SendReqFriend';

export class SendReqList extends BaseComponent {
    constructor(props: any) {
        super(props);
        
        this.nextPage = this.nextPage.bind(this);
        this.previousPage = this.previousPage.bind(this);
        this.cancel = this.cancel.bind(this);

        this.state = {
            errorMessage: '',
            UsersData: {
                totalCount: 0,
                pageSize: 6,
                currentPage: 1,
                previousPage: "No",
                nextPage: "No",
                users: []
            },
            authUser: JSON.parse(AuthService.getUserData())
        }
    }

    componentDidMount() {
        FriendService.getFriendRequestMadePage(this.state.authUser.id, this.state.UsersData.currentPage, this.state.UsersData.pageSize).then(
            (friends) => {
                this.setState({
                    UsersData: friends
                })
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({errorMessage: error.response.data})
                else if (error.message)
                    this.setState({errorMessage: error.message})

            }
        )
    }

    nextPage() {
        FriendService.getFriendRequestMadePage(this.state.authUser.id, this.state.UsersData.currentPage + 1, this.state.UsersData.pageSize).then(
            (friends) => {
                this.setState({
                    UsersData: friends
                })
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({errorMessage: error.response.data})
                else if (error.message)
                    this.setState({errorMessage: error.message})

            }
        )
    }

    previousPage() {
        FriendService.getFriendRequestMadePage(this.state.authUser.id, this.state.UsersData.currentPage - 1, this.state.UsersData.pageSize).then(
            (friends) => {
                this.setState({
                    UsersData: friends
                })
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({errorMessage: error.response.data})
                else if (error.message)
                    this.setState({errorMessage: error.message})

            }
        )
    }

    cancel(id1: number, id2: number) {
        FriendService.deleteFriendship(id1, id2).then(
            () =>{
                alert("Request canceled");
                FriendService.getFriendRequestMadePage(this.state.authUser.id, this.state.UsersData.currentPage, this.state.UsersData.pageSize).then(
                    (friends) => {
                        this.setState({
                            UsersData: friends
                        })
                    },
                    (error) => {
                        if (error && error.response && error.response.data)
                            this.setState({ errorMessage: error.response.data, UsersData: {
                                totalCount: this.state.UsersData.totalCount,
                                pageSize: this.state.UsersData.pageSize,
                                currentPage: this.state.UsersData.currentPage,
                                previousPage: this.state.UsersData.previousPage,
                                nextPage: this.state.UsersData.nextPage,
                                users: []
                            }})
                        else if (error.message)
                            this.setState({ errorMessage: error.message })
                    }
            )},
            (error) => 
            FriendService.getFriendRequestMadePage(this.state.authUser.id, this.state.UsersData.currentPage, this.state.UsersData.pageSize).then(
                (friends) => {
                    this.setState({
                        UsersData: friends
                    })
                },
                (error) => {
                    if (error && error.response && error.response.data)
                        this.setState({ errorMessage: error.response.data })
                    else if (error.message)
                        this.setState({ errorMessage: error.message })
                }
            ))
    }

    render() {
        let users = this.state.UsersData.users;
        let users1 = [];
        let users2 = [];
        let users3 = [];

        for (var i = 0; i < this.state.UsersData.users.length; i++) {
            if ((i % 3) == 0)
                users1.push(users[i]);
            else if ((i % 3) == 1)
                users2.push(users[i]);
            else
                users3.push(users[i]);
        }

        return (
            <div>
                <p> {this.state.errorMessage} </p>
                {(this.state.UsersData.users.length === 0) ?
                    <p>
                         {/* There are no friends  */}
                    </p>
                    :
                    <div className="row">
                        <div className="col-sm-4">
                            {
                                users1.map((friend: any, i: number) =>
                                    <SendReqFriend UserData={friend} key={friend.id} cancel = {this.cancel} />
                                )}
                        </div>
                        <div className="col-sm-4">
                            {
                                users2.map((friend: any, i: number) =>
                                    <SendReqFriend UserData={friend} key={friend.id} cancel = {this.cancel} />
                                )}
                        </div>
                        <div className="col-sm-4">
                            {
                                users3.map((friend: any, i: number) =>
                                    <SendReqFriend UserData={friend} key={friend.id} cancel = {this.cancel} />
                                )}
                        </div>
                    </div>
                }

                <button
                    className="btn btn-danger glyphicon glyphicon-danger"
                    disabled={(this.state.UsersData.previousPage === "No")}
                    onClick={this.previousPage}
                > Previous Page </button>
                <button
                    className="btn btn-danger glyphicon glyphicon-danger"
                    disabled={(this.state.UsersData.nextPage === "No")}
                    onClick={this.nextPage}
                > Next Page </button>

            </div>
        );
    }
}
