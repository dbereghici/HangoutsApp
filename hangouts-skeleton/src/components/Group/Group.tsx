import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
import BaseComponent from '../BaseComponent/BaseComponent';
import { Header } from '../Header/Header';
import GroupService from '../../services/GroupService';
import { UsersService } from '../../services/UsersService';
import { User } from '../User/User';
import AuthService from '../../services/AuthService';
import AdministrateGroup from './AdministrateGroup';
import { MembersList } from './MembersList';
import { Redirect } from 'react-router';

export class Group extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.refresh = this.refresh.bind(this);
        this.showMembersOrPlans = this.showMembersOrPlans.bind(this);
        this.deleteGroup = this.deleteGroup.bind(this);
        this.sendRequest = this.sendRequest.bind(this);
        this.deleteRequest = this.deleteRequest.bind(this);
        this.acceptRequest = this.acceptRequest.bind(this);
        this.redirectToPlan = this.redirectToPlan.bind(this);

        this.state = {
            errorMessage: '',
            showMembersOrPlans: '',
            redirectToGroup: false,
            redirectToPlan: false,
            admin: {
                id: 0,
                username: '',
                firstName: '',
                lastName: '',
                age: 0,
                address: '',
                relationshipStatus: ''
            },
            group: {
                id: 0,
                admin: '',
                adminId: 0,
                Name: '',
                nrOfMembers: 0
            }
        }
    }

    componentDidMount() {
        GroupService.getGroup(this.props.match.params.id).then(
            (group) => {
                this.setState({
                    group: group,
                    errorMessage: ''
                });
                UsersService.getUser(group.adminID).then(
                    (user) => {
                        this.setState({
                            admin: user
                        });
                        UsersService.GetFriendshipStatus(JSON.parse(AuthService.getUserData()).id, this.state.admin.id).then(
                            (status) => {
                                this.setState({
                                    admin: { ...this.state.admin, relationshipStatus: status }
                                })
                            }
                        )
                    },
                    (error) => {
                        if (error && error.response && error.response.data)
                            this.setState({ errorMessage: error.response.data })
                        else if (error.message)
                            this.setState({ errorMessage: error.message })
                    }
                );
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        );
    }

    deleteGroup() {
        GroupService.deleteGroup(this.state.group.id).then(
            () => {
                alert("The groups has been succesfully deleted");
                this.setState({
                    redirectToGroup: true
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
    }

    acceptRequest() {
        GroupService.updateUserGroup(JSON.parse(AuthService.getUserData()).id, this.state.group.id, "member").then(
            () => {
                this.setState({
                    group : {...this.state.group, status : "member"}
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
    }

    deleteRequest() {
        GroupService.deleteUserGroup(JSON.parse(AuthService.getUserData()).id, this.state.group.id).then(
            () => {
                this.setState({
                    group : {...this.state.group, status : ""}
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
    }

    sendRequest() {
        GroupService.addUserToGroup(JSON.parse(AuthService.getUserData()).id, this.state.group.id, "sent").then(
            () => {
                this.setState({
                    group : {...this.state.group, status : "sent"}
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
    }

    refresh() {
        // UsersService.GetAllUsersWithRelationStatusPage(JSON.parse(AuthService.getUserData()).id, this.state.UsersData.currentPage, this.state.UsersData.pageSize).then(
        UsersService.getUser(this.state.group.adminID).then(
            (user) => {
                this.setState({
                    admin: user
                });
                UsersService.GetFriendshipStatus(JSON.parse(AuthService.getUserData()).id, this.state.admin.id).then(
                    (status) => {
                        this.setState({
                            admin: { ...this.state.admin, relationshipStatus: status }
                        })
                    }
                )
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        );
    }

    showMembersOrPlans(type: string) {
        this.setState({
            showMembersOrPlans: type
        })
    }

    showError() {

    }

    redirectToPlan(){
        this.setState({
            redirectToPlan: true
        })
    }

    render() {
        if (this.state.redirectToGroup) {
            let redirectTo = '/groups/';
            return <Redirect to={redirectTo} />;
        }
        if (this.state.redirectToPlan) {
            let redirectTo ='/plan/group/' + this.state.group.id;
            return <Redirect to={redirectTo} />;
        }
        return (
            <div>
                <Header />
                <p> {this.state.errorMessage} </p>
                {/* <p> Group ID => {this.props.match.params.id} </p> */}

                <div className="container-fluid text-center">
                    <div className="row content">
                        <div className="col-sm-2 sidenav">
                            <h2><b>Admin:</b></h2>
                            <User
                                UserData={this.state.admin}
                                key={this.state.admin.id}
                                refresh={this.refresh}
                                showError={this.showError}
                            />


                        </div>
                        <div className="col-sm-8 text-left">
                            <h2> {this.state.group.name} </h2>
                            <p> <b>  {this.state.group.nrOfMembers} members</b> </p>
                            <hr />
                            {/* <h3>Test</h3> */}
                            {/* <UsersList groupId={this.state.group.id}/> */}

                            <h3> What are you up for today, {JSON.parse(AuthService.getUserData()).firstName} ? </h3>
                            <button
                                className="btn-lg btn-info"
                                onClick={this.redirectToPlan}
                            > Create a plan </button>
                            <br/><br/><br/>

                            <button
                                className="btn btn-success"
                                onClick={() => this.showMembersOrPlans("members")}
                            > Members </button>
                            <button
                                className="btn btn-success"
                                onClick={() => this.showMembersOrPlans("plans")}
                            > Plans </button>
                            {this.state.showMembersOrPlans === "members" ?
                                <MembersList groupId={this.state.group.id} />
                                : this.state.showMembersOrPlans === "plans" ?
                                    //<UsersList groupId={this.state.group.id}/>
                                    <p> plans </p>
                                    : <div />}
                        </div>
                        <div className="col-sm-2 sidenav">
                            <div className="well">
                                {
                                    this.state.group.status ?
                                        //my groups
                                        this.state.group.status === "admin" ?
                                            //admin
                                            <div>
                                                <p>You are the admin of this group </p>
                                                <button
                                                    className="btn btn-warning glyphicon glyphicon-remove"
                                                    onClick={this.deleteGroup}
                                                > Delete Group
                                    </button>
                                            </div>
                                            :
                                            this.state.group.status === "received" ?
                                                //received
                                                <div>
                                                    <p>You are invited to join this group</p>
                                                    <button
                                                        className="btn btn-warning glyphicon glyphicon-remove"
                                                        onClick={this.acceptRequest}
                                                    > Accept
                                    </button>
                                                    <button
                                                        className="btn btn-warning glyphicon glyphicon-remove"
                                                        onClick={this.deleteRequest}
                                                    > Decline
                                    </button>
                                                </div>
                                                :
                                                this.state.group.status === "sent" ?
                                                    //sent
                                                    <div>
                                                        <p>You have sent a request to join this group</p>
                                                        <button
                                                            className="btn btn-warning glyphicon glyphicon-remove"
                                                            onClick={this.deleteRequest}
                                                        > Cancel
                                            </button>
                                                    </div>
                                                    :
                                                    <div>
                                                        <p>You are member of this group</p>
                                                        <button
                                                            className="btn btn-warning glyphicon glyphicon-remove"
                                                            onClick={this.deleteRequest}
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
                            </div>
                        </div>
                    </div>
                </div>

                {this.state.group.status === "admin" ? <AdministrateGroup groupId={this.state.group.id} /> : <div />}


            </div >
        );
    }
}
