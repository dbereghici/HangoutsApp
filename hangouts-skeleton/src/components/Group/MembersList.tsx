import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
import BaseComponent from '../BaseComponent/BaseComponent';
import AuthService from '../../services/AuthService';
import { UsersService } from '../../services/UsersService';
import { User } from '../User/User';
import { Redirect } from 'react-router';

export class MembersList extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.nextPage = this.nextPage.bind(this);
        this.previousPage = this.previousPage.bind(this);
        this.handleSearchInput = this.handleSearchInput.bind(this);
        this.search = this.search.bind(this);
        this.refresh = this.refresh.bind(this);
        this.showError = this.showError.bind(this);

        this.state = {
            func: this.props.getUsers,
            errorMessage: '',
            redirectToChat: false,
            searchString: '',
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
        this.props;    
        debugger;
        UsersService.getAllUsersFromGroup(this.props.groupId, this.state.authUser.id, 
            this.state.searchString, 1, 6).then(
            (usersData: any) => {
                this.setState({
                    UsersData: usersData
                })
            },
            (error: any) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        )
    }

    nextPage() {
        UsersService.getAllUsersFromGroup(this.props.groupId, this.state.authUser.id, 
            this.state.searchString, this.state.UsersData.currentPage + 1, this.state.UsersData.pageSize).then(
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
        UsersService.getAllUsersFromGroup(this.props.groupId, this.state.authUser.id, 
            this.state.searchString, this.state.UsersData.currentPage - 1, this.state.UsersData.pageSize).then(
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

    search(event: any){
        event.preventDefault();
        UsersService.getAllUsersFromGroup(this.props.groupId, this.state.authUser.id, 
            this.state.searchString, 1, this.state.UsersData.pageSize).then(    (friends) => {
                this.setState({
                    UsersData: friends,
                    errorMessage: ''
                })
            },
            (error) => {
                if (error && error.response && error.response.data){
                    this.setState({errorMessage: error.response.data})
                }
                else if (error.message)
                    this.setState({errorMessage: error.message})
                
            }
        )
    }

    handleSearchInput(e: any) {
        const q = e.target.value;
        this.setState({
            searchString: q
        });
    }

    refresh(){
        UsersService.getAllUsersFromGroup(this.props.groupId, this.state.authUser.id, 
            this.state.searchString, this.state.UsersData.currentPage, this.state.UsersData.pageSize).then(    (friends) => {
                // alert("Operation succesfull");
                this.setState({
                    UsersData: friends
                })
            });
    }

    showError(error: any){
        this.setState({errorMessage: error})
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
        if (this.state.redirectToChat) {
            let redirectTo = '/chat/user/' + this.state.UserData.id;
            return <Redirect to={redirectTo} />;
        }
        return (
            <div>
                <h1> Members </h1>
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
                        {/* There are no friends  */}
                    </p>
                    :
                    <div className="row">
                        <div className="col-sm-4">
                            {
                                users1.map((friend: any, i: number) =>
                                    <User 
                                        UserData={friend} 
                                        key={friend.id} 
                                        showError = {this.showError}
                                        refresh = {this.refresh}
                                    />
                                )}
                        </div>
                        <div className="col-sm-4">
                            {
                                users2.map((friend: any, i: number) =>
                                <User 
                                    UserData={friend} 
                                    key={friend.id} 
                                    showError = {this.showError}
                                    refresh = {this.refresh}
                                />
                                )}
                        </div>
                        <div className="col-sm-4">
                            {
                                users3.map((friend: any, i: number) =>
                                    <User 
                                        UserData={friend} 
                                        key={friend.id} 
                                        showError = {this.showError}
                                        refresh = {this.refresh}
                                    />
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
