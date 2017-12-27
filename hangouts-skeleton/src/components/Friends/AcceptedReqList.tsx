import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
// import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import './Friends.css'
import FriendService from '../../services/FriendsService';
import AuthService from '../../services/AuthService';
import { AcceptedReqFriend } from './AcceptedReqFriend';

export class AcceptedReqList extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.nextPage = this.nextPage.bind(this);
        this.previousPage = this.previousPage.bind(this);
        this.unfriend = this.unfriend.bind(this);

        this.state = {
            errorMessage: '',
            UsersData: {
                totalCount: 0,
                pageSize: 6,
                currentPage: 1,
                previousPage: "No",
                nextPage: "No",
                users: []
            }
        }
    }

    componentDidMount() {
        FriendService.getFriendsPage(JSON.parse(AuthService.getUserData()).id, this.state.UsersData.currentPage, this.state.UsersData.pageSize).then(
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
        )
    }

    nextPage() {
        FriendService.getFriendsPage(JSON.parse(AuthService.getUserData()).id, this.state.UsersData.currentPage + 1, this.state.UsersData.pageSize).then(
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
        )
    }

    previousPage() {
        FriendService.getFriendsPage(JSON.parse(AuthService.getUserData()).id, this.state.UsersData.currentPage - 1, this.state.UsersData.pageSize).then(
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
        )
    }

    unfriend(id1: number, id2: number) {
        FriendService.deleteFriendship(id1, id2).then(
            () => {
                FriendService.getFriendsPage(JSON.parse(AuthService.getUserData()).id, this.state.UsersData.currentPage, this.state.UsersData.pageSize).then(
                    (friends) => {
                        this.setState({
                            UsersData: friends
                        });
                        alert("Operation successfull");
                        this.forceUpdate();
                    },
                    (error) => {
                        if (error && error.response && error.response.data)
                            this.setState({ errorMessage: error.response.data })
                        else if (error.message)
                            this.setState({ errorMessage: error.message })
                    }
                )
            },
            (error) =>                 
            FriendService.getFriendsPage(JSON.parse(AuthService.getUserData()).id, this.state.UsersData.currentPage, this.state.UsersData.pageSize).then(
                (friends) => {
                    this.setState({
                        UsersData: friends
                    });
                    alert("Operation successfull");
                    this.forceUpdate();
                },
                (error) => {
                    if (error && error.response && error.response.data)
                        this.setState({ errorMessage: error.response.data })
                    else if (error.message)
                        this.setState({ errorMessage: error.message })
                }
            )
        );
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
                                    <AcceptedReqFriend UserData={friend} key={friend.id} unfriend={this.unfriend} />
                                )}
                        </div>
                        <div className="col-sm-4">
                            {
                                users2.map((friend: any, i: number) =>
                                    <AcceptedReqFriend UserData={friend} key={friend.id} unfriend={this.unfriend} />
                                )}
                        </div>
                        <div className="col-sm-4">
                            {
                                users3.map((friend: any, i: number) =>
                                    <AcceptedReqFriend UserData={friend} key={friend.id} unfriend={this.unfriend} />
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
