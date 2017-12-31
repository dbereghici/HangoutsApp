import * as React from 'react';
// import 'bootstrap/dist/css/bootstrap.css';
// import 'bootstrap/dist/css/bootstrap-theme.css';
// import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import { Header } from '../Header/Header';
import { SendReqList } from './SendReqList';
import { ReceivedReqList } from './ReceivedReqList';
import { AcceptedReqList } from './AcceptedReqList';
import { SearchFriendList } from './SearchFriendList';

export class Friends extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.setFriendsTypeToAccepted = this.setFriendsTypeToAccepted.bind(this);
        this.setFriendsTypeToReceived = this.setFriendsTypeToReceived.bind(this);
        this.setFriendsTypeToSearch = this.setFriendsTypeToSearch.bind(this);
        this.setFriendsTypeToSend = this.setFriendsTypeToSend.bind(this);

        this.state = {
            friendsType: "accepted"
        }
    }

    setFriendsTypeToAccepted() {
        this.setState({
            friendsType: "accepted"
        })
    }

    setFriendsTypeToReceived() {
        this.setState({
            friendsType: "received"
        })
    }

    setFriendsTypeToSearch() {
        this.setState({
            friendsType: "search"
        })
    }

    setFriendsTypeToSend() {
        this.setState({
            friendsType: "send"
        })
    }

    render() {
        // if (!this.state.isAuth) {
        //     return <Redirect to='/authentication' />;
        // }

        return (
            <div>
                <Header />

                <div className="container">
                    <button type="button" className="btn btn-success" onClick={this.setFriendsTypeToAccepted}> All friends </button>
                    <button type="button" className="btn btn-success" onClick={this.setFriendsTypeToSend}> Friend Requests Sent </button>
                    <button type="button" className="btn btn-success" onClick={this.setFriendsTypeToReceived}> Friend Requests Received </button>
                    <button type="button" className="btn btn-success" onClick={this.setFriendsTypeToSearch}> Search</button>

                    {this.state.friendsType === "accepted" ?
                        <div>
                            <h2> All Friends </h2>
                            <AcceptedReqList />
                        </div>
                        :
                        this.state.friendsType === "send" ?
                            <div>
                                <h2> Friend Requests Sent </h2>
                                <SendReqList />
                            </div>
                            :
                            this.state.friendsType === "received" ?
                                <div>
                                    <h2> Friend Requests Received </h2>
                                    <ReceivedReqList />
                                </div>

                                :
                                <div>
                                    <h2> Search For New Friends </h2>
                                    <SearchFriendList />
                                </div>
                    }
                </div>
            </div>
        );
    }
}
