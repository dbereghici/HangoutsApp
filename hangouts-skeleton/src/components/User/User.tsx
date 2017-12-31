import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
import BaseComponent from '../BaseComponent/BaseComponent';
import { AcceptedReqFriend } from '../Friends/AcceptedReqFriend';
import { SendReqFriend } from '../Friends/SendReqFriend';
import { SearchFriend } from '../Friends/SearchFriend';
import { ReceivedReqFriend } from '../Friends/ReceivedReqFriend';
import FriendService from '../../services/FriendsService';

export class User extends BaseComponent {
    constructor(props: any) {
        super(props);
        
        this.state = {
            UserData : this.props.UserData
        }

        this.unfriend = this.unfriend.bind(this);
        this.addFriend = this.addFriend.bind(this);
        this.accept = this.accept.bind(this);
    }


    unfriend(id1: number, id2: number) {
        FriendService.deleteFriendship(id1, id2).then(
            () => {
                this.props.refresh();
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.props.showError(error.response.data);
                else if (error.message)
                    this.props.showError(error.message);
            }
        )
    }

    accept(id1: number, id2: number) {
        FriendService.updateFriendship(id1, id2, "accepted").then(
            () => {
                this.props.refresh();
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.props.showError(error.response.data);
                else if (error.message)
                    this.props.showError(error.message);
            }
        )
    }

    addFriend(id1: number, id2: number) {
        FriendService.addFriendship(id1, id2).then(
            () => {
                this.props.refresh();
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.props.showError(error.response.data);
                else if (error.message)
                    this.props.showError(error.message);
            }
        )
    }

    message() {
        this.setState({
            redirectToChat: true
        })
    }

    componentWillReceiveProps(nextProps: any){
        this.setState({
            UserData : nextProps.UserData
        })

    }

    render() {
        if (this.state.UserData.relationshipStatus === "accepted"){
            return (
                <AcceptedReqFriend  UserData={this.state.UserData} key={this.state.UserData.id} unfriend={this.unfriend}/>
            )
        }
        if (this.state.UserData.relationshipStatus === "sent"){
            return (
                <SendReqFriend  UserData={this.state.UserData} key={this.state.UserData.id} cancel={this.unfriend}/>
            )
        }
        if (this.state.UserData.relationshipStatus === "received"){
            return (
                <ReceivedReqFriend  UserData={this.state.UserData} key={this.state.UserData.id} accept={this.accept} decline={this.unfriend}/>
            )
        }
        return (
            <SearchFriend  UserData={this.state.UserData} key={this.state.UserData.id} addFriend={this.addFriend}/>
        );
    }
}
