import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
// import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import { GroupPanel } from './GroupPanel';
import GroupService from '../../services/GroupService';
import AuthService from '../../services/AuthService';

export class AllGroupsList extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.nextPage = this.nextPage.bind(this);
        this.previousPage = this.previousPage.bind(this);
        this.handleSearchInput = this.handleSearchInput.bind(this);
        this.search = this.search.bind(this);
        // this.unfriend = this.unfriend.bind(this);
        this.sendRequest = this.sendRequest.bind(this);
        this.acceptRequest = this.acceptRequest.bind(this);
        this.deleteRequest = this.deleteRequest.bind(this);
        this.deleteGroup = this.deleteGroup.bind(this);

        this.state = {
            errorMessage: '',
            searchString: '',
            GroupData: {
                totalCount: 0,
                pageSize: 6,
                currentPage: 1,
                previousPage: "No",
                nextPage: "No",
                groups: []
            },
            authUser: JSON.parse(AuthService.getUserData())
        }
    }

    componentDidMount() {
        GroupService.getAllGroupsPage(this.state.authUser.id, "", this.state.GroupData.currentPage, this.state.GroupData.pageSize).then(
            (groupData) => {
                this.setState({
                    GroupData: groupData
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
        GroupService.getAllGroupsPage(this.state.authUser.id, this.state.searchString, this.state.GroupData.currentPage + 1, this.state.GroupData.pageSize).then(
            (groupData) => {
                this.setState({
                    GroupData: groupData
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
        GroupService.getAllGroupsPage(this.state.authUser.id, this.state.searchString, this.state.GroupData.currentPage - 1, this.state.GroupData.pageSize).then(
            (groupData) => {
                this.setState({
                    GroupData: groupData
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

    handleSearchInput(e: any) {
        const q = e.target.value;
        this.setState({
            searchString: q
        });
    }

    search(event: any) {
        event.preventDefault();
        const q = event.target.value;
        if(!!q)
            this.setState({
                searchString: q
            });
        GroupService.getAllGroupsPage(this.state.authUser.id, this.state.searchString, 1, this.state.GroupData.pageSize).then(
            (groupData) => {
                this.setState({
                    GroupData: groupData,
                    errorMessage: ''
                })
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message) {
                    this.setState({ errorMessage: error.message })
                }
            }
        )
    }

    sendRequest(groupId: number){
        GroupService.addUserToGroup(this.state.authUser.id, groupId, "sent").then(
            () => {
                GroupService.getAllGroupsPage(this.state.authUser.id, this.state.searchString, this.state.GroupData.currentPage, this.state.GroupData.pageSize).then(
                    (groupData) => {
                        this.setState({
                            GroupData: groupData,
                            errorMessage: ''
                        })
                    },
                    (error) => {
                        if (error && error.response && error.response.data)
                            this.setState({ errorMessage: error.response.data })
                        else if (error.message) {
                            this.setState({ errorMessage: error.message })
                        }
                    }
                )
            }, 
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message) {
                    this.setState({ errorMessage: error.message })
                }
            }
        )
    }

    acceptRequest(groupId: number){
        GroupService.updateUserGroup(this.state.authUser.id, groupId, "member").then(
            () => {
                GroupService.getAllGroupsPage(this.state.authUser.id, this.state.searchString, this.state.GroupData.currentPage, this.state.GroupData.pageSize).then(
                    (groupData) => {
                        this.setState({
                            GroupData: groupData,
                            errorMessage: ''
                        })
                    },
                    (error) => {
                        if (error && error.response && error.response.data)
                            this.setState({ errorMessage: error.response.data })
                        else if (error.message) {
                            this.setState({ errorMessage: error.message })
                        }
                    }
                )
            }, 
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message) {
                    this.setState({ errorMessage: error.message })
                }
            }
        )
    }

    deleteRequest(groupId: number){
        GroupService.deleteUserGroup(this.state.authUser.id, groupId).then(
            () => {
                GroupService.getAllGroupsPage(this.state.authUser.id, this.state.searchString, this.state.GroupData.currentPage, this.state.GroupData.pageSize).then(
                    (groupData) => {
                        this.setState({
                            GroupData: groupData,
                            errorMessage: ''
                        })
                    },
                    (error) => {
                        if (error && error.response && error.response.data)
                            this.setState({ errorMessage: error.response.data })
                        else if (error.message) {
                            this.setState({ errorMessage: error.message })
                        }
                    }
                )
            }, 
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message) {
                    this.setState({ errorMessage: error.message })
                }
            }
        )
    }

    deleteGroup(groupId: number){
        GroupService.deleteGroup(groupId).then(
            () => {
                GroupService.getAllGroupsPage(this.state.authUser.id, this.state.searchString, this.state.GroupData.currentPage, this.state.GroupData.pageSize).then(
                    (groupData) => {
                        this.setState({
                            GroupData: groupData,
                            errorMessage: ''
                        })
                    },
                    (error) => {
                        if (error && error.response && error.response.data)
                            this.setState({ errorMessage: error.response.data })
                        else if (error.message) {
                            this.setState({ errorMessage: error.message })
                        }
                    }
                )
            }, 
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message) {
                    this.setState({ errorMessage: error.message })
                }
            }
        )
    }
    

    render() {
        let groups = this.state.GroupData ? this.state.GroupData.groups : null;

        if (!groups) {
            return <div></div>

        }

        let groups1 = [];
        let groups2 = [];
        let groups3 = [];

        for (var i = 0; i < this.state.GroupData.groups.length; i++) {
            if ((i % 3) == 0)
                groups1.push(groups[i]);
            else if ((i % 3) == 1)
                groups2.push(groups[i]);
            else
                groups3.push(groups[i]);
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
                {(this.state.GroupData.groups.length === 0) ?
                    <p>
                        {/* There are no friends  */}
                    </p>
                    :
                    <div className="row">
                        <div className="col-sm-4">
                            {
                                groups1.map((group: any, i: number) =>
                                    <GroupPanel GroupData={group} 
                                        key={group.id} 
                                        status={group.status} 
                                        sendRequest={this.sendRequest}
                                        deleteRequest={this.deleteRequest}
                                        acceptRequest={this.acceptRequest}
                                        deleteGroup={this.deleteGroup}
                                    />
                                )}
                        </div>
                        <div className="col-sm-4">
                            {
                                groups2.map((group: any, i: number) =>
                                    <GroupPanel GroupData={group} 
                                        key={group.id} 
                                        status={group.status} 
                                        sendRequest={this.sendRequest}
                                        deleteRequest={this.deleteRequest}
                                        acceptRequest={this.acceptRequest}
                                        deleteGroup={this.deleteGroup}                                        
                                    />
                                )}
                        </div>
                        <div className="col-sm-4">
                            {
                                groups3.map((group: any, i: number) =>
                                    <GroupPanel GroupData={group} 
                                        key={group.id} 
                                        status={group.status} 
                                        sendRequest={this.sendRequest}
                                        deleteRequest={this.deleteRequest}
                                        acceptRequest={this.acceptRequest}
                                        deleteGroup={this.deleteGroup}                                        
                                    />
                                )}
                        </div>
                    </div>
                }

                <button
                    className="btn btn-danger glyphicon glyphicon-danger"
                    disabled={(this.state.GroupData.previousPage === "No")}
                    onClick={this.previousPage}
                > Previous Page </button>
                <button
                    className="btn btn-danger glyphicon glyphicon-danger"
                    disabled={(this.state.GroupData.nextPage === "No")}
                    onClick={this.nextPage}
                > Next Page </button>

            </div>
        );
    }
}
