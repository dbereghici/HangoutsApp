import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
// import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import { GroupPanel } from './GroupPanel';
import GroupService from '../../services/GroupService';
import AuthService from '../../services/AuthService';

export class MyGroupsList extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.nextPage = this.nextPage.bind(this);
        this.previousPage = this.previousPage.bind(this);
        this.handleSearchInput = this.handleSearchInput.bind(this);
        this.search = this.search.bind(this);
        this.acceptRequest = this.acceptRequest.bind(this);
        this.deleteGroup = this.deleteGroup.bind(this);
        this.deleteRequest = this.deleteRequest.bind(this);
        this.sendRequest  = this.sendRequest.bind(this);
        // this.unfriend = this.unfriend.bind(this);

        this.state = {
            errorMessage: '',
            searchString: '',
            status: this.props.status,
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

    componentWillReceiveProps(nextProps: any) {
        this.setState({ status: nextProps.status })
        GroupService.getMyGroupsPage("", nextProps.status, this.state.authUser.id, this.state.GroupData.currentPage, this.state.GroupData.pageSize).then(
            (groupData) => {
                this.setState({
                    GroupData: groupData
                })
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data, GroupData: { ...this.state.GroupData, groups: [] } })
                else if (error.message)
                    this.setState({ errorMessage: error.message, GroupData: { ...this.state.GroupData, groups: [] } })
            }
        )
        this.forceUpdate();
    }


    componentDidMount() {
        GroupService.getMyGroupsPage("", this.props.status, this.state.authUser.id, this.state.GroupData.currentPage, this.state.GroupData.pageSize).then(
            (groupData) => {
                this.setState({
                    GroupData: groupData
                })
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data, GroupData: { ...this.state.GroupData, groups: [] } })
                else if (error.message)
                    this.setState({ errorMessage: error.message, GroupData: { ...this.state.GroupData, groups: [] } })
            }
        )
    }

    nextPage() {
        GroupService.getMyGroupsPage(this.state.searchString, this.props.status, this.state.authUser.id, this.state.GroupData.currentPage + 1, this.state.GroupData.pageSize).then(
            (groupData) => {
                this.setState({
                    GroupData: groupData
                })
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data, GroupData: { ...this.state.GroupData, groups: [] } })
                else if (error.message)
                    this.setState({ errorMessage: error.message, GroupData: { ...this.state.GroupData, groups: [] } })
            }
        )
    }

    previousPage() {
        GroupService.getMyGroupsPage(this.state.searchString, this.props.status, this.state.authUser.id, this.state.GroupData.currentPage - 1, this.state.GroupData.pageSize).then(
            (groupData) => {
                this.setState({
                    GroupData: groupData
                })
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data, GroupData: { ...this.state.GroupData, groups: [] } })
                else if (error.message)
                    this.setState({ errorMessage: error.message, GroupData: { ...this.state.GroupData, groups: [] } })
            }
        )
    }

    handleSearchInput(e: any) {
        const q = e.target.value;
        this.setState({
            searchString: q
        });
    }

    sendRequest(groupId: number) {
        GroupService.addUserToGroup(this.state.authUser.id, groupId, "sent").then(
            () => {
                GroupService.getMyGroupsPage("", this.state.status, this.state.authUser.id, this.state.GroupData.currentPage, this.state.GroupData.pageSize).then(
                    (groupData) => {
                        this.setState({
                            GroupData: groupData,
                            errorMessage: ''
                        })
                    },
                    (error) => {
                        if (error && error.response && error.response.data)
                            this.setState({ errorMessage: error.response.data, GroupData: { ...this.state.GroupData, groups: [] } })
                        else if (error.message) {
                            this.setState({ errorMessage: error.message, GroupData: { ...this.state.GroupData, groups: [] } })
                        }
                    }
                )
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data, GroupData: { ...this.state.GroupData, groups: [] } })
                else if (error.message) {
                    this.setState({ errorMessage: error.message, GroupData: { ...this.state.GroupData, groups: [] } })
                }
            }
        )
    }

    acceptRequest(groupId: number) {
        GroupService.updateUserGroup(this.state.authUser.id, groupId, "member").then(
            () => {
                GroupService.getMyGroupsPage("", this.state.status, this.state.authUser.id, this.state.GroupData.currentPage, this.state.GroupData.pageSize).then(
                    (groupData) => {
                        this.setState({
                            GroupData: groupData,
                            errorMessage: ''
                        })
                    },
                    (error) => {
                        if (error && error.response && error.response.data)
                            this.setState({ errorMessage: error.response.data, GroupData: { ...this.state.GroupData, groups: [] } })
                        else if (error.message) {
                            this.setState({ errorMessage: error.message, GroupData: { ...this.state.GroupData, groups: [] } })
                        }
                    }
                )
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data, GroupData: { ...this.state.GroupData, groups: [] } })
                else if (error.message) {
                    this.setState({ errorMessage: error.message, GroupData: { ...this.state.GroupData, groups: [] } })
                }
            }
        )
    }

    deleteRequest(groupId: number) {
        GroupService.deleteUserGroup(this.state.authUser.id, groupId).then(
            () => {
                GroupService.getMyGroupsPage("", this.state.status, this.state.authUser.id, this.state.GroupData.currentPage, this.state.GroupData.pageSize).then(
                    (groupData) => {
                        this.setState({
                            GroupData: groupData,
                            errorMessage: ''
                        })
                    },
                    (error) => {
                        if (error && error.response && error.response.data)
                            this.setState({ errorMessage: error.response.data, GroupData: { ...this.state.GroupData, groups: [] } })
                        else if (error.message) {
                            this.setState({ errorMessage: error.message, GroupData: { ...this.state.GroupData, groups: [] } })
                        }
                    }
                )
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data, GroupData: { ...this.state.GroupData, groups: [] } })
                else if (error.message) {
                    this.setState({ errorMessage: error.message, GroupData: { ...this.state.GroupData, groups: [] } })
                }
            }
        )
    }

    deleteGroup(groupId: number) {
        GroupService.deleteGroup(groupId).then(
            () => {
                GroupService.getMyGroupsPage("", this.state.status, this.state.authUser.id, this.state.GroupData.currentPage, this.state.GroupData.pageSize).then(
                    (groupData) => {
                        this.setState({
                            GroupData: groupData,
                            errorMessage: ''
                        })
                    },
                    (error) => {
                        if (error && error.response && error.response.data)
                            this.setState({ errorMessage: error.response.data, GroupData: { ...this.state.GroupData, groups: [] } })
                        else if (error.message) {
                            this.setState({ errorMessage: error.message, GroupData: { ...this.state.GroupData, groups: [] } })
                        }
                    }
                )
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data, GroupData: { ...this.state.GroupData, groups: [] } })
                else if (error.message) {
                    this.setState({ errorMessage: error.message, GroupData: { ...this.state.GroupData, groups: [] } })
                }
            }
        )
    }

    search(event: any) {
        event.preventDefault();
        const q = event.target.value;
        if(!!q)
            this.setState({
                searchString: q
            });
        GroupService.getMyGroupsPage(this.state.searchString, this.props.status, this.state.authUser.id, 1, this.state.GroupData.pageSize).then(
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
                {this.state.status === "member" ?
                    <h2> My groups </h2>
                    :
                    this.state.status === "admin" ?
                        <h2> Groups Administrated </h2>
                        :
                        this.state.status === "sent" ?
                            <h2> Group Invitation Sent </h2>
                            :
                            <h2> Group Invitation Received</h2>
                }
                <form className="demoForm"
                    onSubmit={this.search}
                >
                    <div>
                        <label htmlFor="text">Search</label>
                        <input type="text" className="form-control"
                            name="searchString" 
                            // value={this.state.searchString}
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
                                        status={this.props.status}
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
                                        status={this.props.status}
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
                                        status={this.props.status}
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
