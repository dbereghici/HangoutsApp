import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
import BaseComponent from '../BaseComponent/BaseComponent';
import './Friends.css'
import FriendService from '../../services/FriendsService';
import AuthService from '../../services/AuthService';
import { SearchFriend } from './SearchFriend';

export class SearchFriendList extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.nextPage = this.nextPage.bind(this);
        this.previousPage = this.previousPage.bind(this);
        this.addFriend = this.addFriend.bind(this);
        this.search = this.search.bind(this);

        this.state = {
            errorMessage: '',
            searchString: '',
            UsersData : {
                totalCount : 0,
                pageSize : 6,
                currentPage : 1,
                previousPage : "No",
                nextPage : "No",
                users : []  
            } 
        }
    }
    
    componentDidMount(){
        FriendService.getNewFriendsSearchPage(JSON.parse(AuthService.getUserData()).id, "", this.state.UsersData.currentPage, this.state.UsersData.pageSize).then(
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

    nextPage(){
        FriendService.getNewFriendsSearchPage(JSON.parse(AuthService.getUserData()).id, "", this.state.UsersData.currentPage + 1, this.state.UsersData.pageSize).then(
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

    previousPage(){
        FriendService.getNewFriendsSearchPage(JSON.parse(AuthService.getUserData()).id, "", this.state.UsersData.currentPage - 1, this.state.UsersData.pageSize).then(
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

    addFriend(id1: number, id2: number){
        FriendService.addFriendship(id1, id2).then(
            () =>{
                alert("Request sent")
                FriendService.getNewFriendsSearchPage(JSON.parse(AuthService.getUserData()).id, "", this.state.UsersData.currentPage, this.state.UsersData.pageSize).then(
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
            },
            (error) => {
                FriendService.getNewFriendsSearchPage(JSON.parse(AuthService.getUserData()).id, "", this.state.UsersData.currentPage, this.state.UsersData.pageSize).then(
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
        );
    }

    handleSearchInput(e: any) {
        const q = e.target.value;
        this.setState({
            searchString: q
        });
    }

    search(event: any){
        event.preventDefault();
        FriendService.getNewFriendsSearchPage(JSON.parse(AuthService.getUserData()).id, this.state.searchString, this.state.UsersData.currentPage, this.state.UsersData.pageSize).then(
            (friends) => {
                this.setState({
                    UsersData: friends,
                    errorMessage: ''
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

    render() {
        let users = this.state.UsersData.users;
        let users1 = [];
        let users2 = [];
        let users3 = [];

        for(var i=0; i < this.state.UsersData.users.length; i++){
            if ( (i % 3) == 0 )
                users1.push(users[i]);
            else if ( (i % 3) == 1) 
                users2.push(users[i]);
            else
                users3.push(users[i]);
        }

        return (
            <div> 
                <form className="demoForm" 
                     onSubmit={this.search}
                >    
                    <div>
                    <label htmlFor="text">Search</label>
                        <input type="text" className="form-control"
                            name="lastname" //value={this.state.lastname} //onChange={(event) => this.handleUserInput(event)} onLoad={(event) => this.handleUserInput(event)}
                            onChange={(event) => this.handleSearchInput(event)}
                        />
                    </div>
                    <button type="submit" className="btn btn-primary">Search</button>
                </form >
                <p> {this.state.errorMessage} </p> 
                 {(this.state.UsersData.users.length === 0) ? 
                    <p> 
                    </p>
                    : 
                    <div className="row">
                        <div className="col-sm-4">
                            {   
                                users1.map((friend: any, i: number)=>
                                <SearchFriend UserData={friend} key={friend.id} addFriend = {this.addFriend} />
                            )}
                        </div>
                        <div className="col-sm-4">
                            {   
                                users2.map((friend: any, i: number)=>
                                <SearchFriend UserData={friend} key={friend.id} addFriend = {this.addFriend} />
                            )}
                        </div>
                        <div className="col-sm-4">
                            {   
                                users3.map((friend: any, i: number)=>
                                <SearchFriend UserData={friend} key={friend.id} addFriend = {this.addFriend} />
                            )}
                        </div>
                    </div>
                }
                
                <button 
                    className="btn btn-danger glyphicon glyphicon-danger" 
                    disabled={(this.state.UsersData.previousPage === "No")}
                    onClick = {this.previousPage}
                > Previous Page </button>
                <button 
                    className="btn btn-danger glyphicon glyphicon-danger" 
                    disabled={(this.state.UsersData.nextPage === "No")}
                    onClick = {this.nextPage}
                > Next Page </button>
                
            </div>
        );
    }
}
