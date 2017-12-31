import * as React from 'react';
import BaseComponent from '../BaseComponent/BaseComponent';
import GroupService from '../../services/GroupService';
import { IGroup } from '../../models/IGroup';
import { FriendsToInviteList } from './FriendsToInviteList';
import { MembersToDeleteList } from './MembersToDeleteList';
import { MembersReqList } from './MembersReqList';
import { FriendsInvitedList } from './FriendsInvitedList';

export default class AdministrateGroup extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.selectUserToInvite = this.selectUserToInvite.bind(this);
        this.selectUserToDelete = this.selectUserToDelete.bind(this);
        this.selectUserToAccept = this.selectUserToAccept.bind(this);
        this.selectUserToCancel = this.selectUserToCancel.bind(this);
        this.invite = this.invite.bind(this);
        this.delete = this.delete.bind(this);
        this.aprove = this.aprove.bind(this);
        this.cancel = this.cancel.bind(this);

        this.state = {
            errorMessage: '',
            usersToAdd: [],
            usersToAddNames: [],
            usersToDelete: [],
            usersToDeleteNames: [],
            usersToAccept: [],
            usersToAcceptNames: [],
            usersToCancel: [],
            usersToCancelNames: [],
            option: ''
        }
    }

    selectUserToInvite(id: number, name: string) {
        if (this.state.usersToAdd.indexOf(id) === -1)
            this.setState({
                usersToAdd: [...this.state.usersToAdd, id],
                usersToAddNames: [...this.state.usersToAddNames, name],
            })
        else {
            this.setState({
                usersToAdd: this.state.usersToAdd.filter((_: any, i: any) => i !== this.state.usersToAdd.indexOf(id)),
                usersToAddNames: this.state.usersToAddNames.filter((_: any, i: any) => i !== this.state.usersToAddNames.indexOf(name))
            })
        }
    }

    selectUserToDelete(id: number, name: string) {
        if (this.state.usersToDelete.indexOf(id) === -1)
            this.setState({
                usersToDelete: [...this.state.usersToDelete, id],
                usersToDeleteNames: [...this.state.usersToDeleteNames, name],
            })
        else {
            this.setState({
                usersToDelete: this.state.usersToDelete.filter((_: any, i: any) => i !== this.state.usersToDelete.indexOf(id)),
                usersToDeleteNames: this.state.usersToDeleteNames.filter((_: any, i: any) => i !== this.state.usersToDeleteNames.indexOf(name))
            })
        }
    }

    selectUserToAccept(id: number, name: string) {
        if (this.state.usersToAccept.indexOf(id) === -1)
            this.setState({
                usersToAccept: [...this.state.usersToAccept, id],
                usersToAcceptNames: [...this.state.usersToAcceptNames, name],
            })
        else {
            this.setState({
                usersToAccept: this.state.usersToAccept.filter((_: any, i: any) => i !== this.state.usersToAccept.indexOf(id)),
                usersToAcceptNames: this.state.usersToAcceptNames.filter((_: any, i: any) => i !== this.state.usersToAcceptNames.indexOf(name))
            })
        }
    }

    selectUserToCancel(id: number, name: string) {
        if (this.state.usersToCancel.indexOf(id) === -1)
            this.setState({
                usersToCancel: [...this.state.usersToCancel, id],
                usersToCancelNames: [...this.state.usersToCancelNames, name],
            })
        else {
            this.setState({
                usersToCancel: this.state.usersToCancel.filter((_: any, i: any) => i !== this.state.usersToCancel.indexOf(id)),
                usersToCancelNames: this.state.usersToCancelNames.filter((_: any, i: any) => i !== this.state.usersToCancelNames.indexOf(name))
            })
        }
    }

    renderNames(name: any, index: number) {
        return (
            <div key={index}>
                <p>
                    <b>{name}</b>
                </p>
            </div>
        )
    }

    setOption(type: string) {
        this.setState({
            option: type
        }, () => this.forceUpdate())
    }

    invite() {
        // this.state.usersToAdd.map((id: any) => this.inviteUserToGroup(id, this.state.groupid));
        this.state.usersToAdd.map((id: any) =>
            GroupService.addUserToGroup(id, this.props.groupId, "received").then(
                (group: IGroup) => {

                },
                (error: any) => {
                    if (error && error.response && error.response.data)
                        this.setState({ errorMessage: error.response.data })
                    else if (error.message)
                        this.setState({ errorMessage: error.message })
                }
            )
        )
        alert("operation succesfull");
        this.setState({
            usersToAdd: [],
            usersToAddNames: [],
            usersToDelete: [],
            usersToDeleteNames: [],
            usersToAccept: [],
            usersToAcceptNames: [],
            usersToCancel: [],
            usersToCancelNames: [],
            option: ''
        });
    }

    delete() {
        this.state.usersToDelete.map((id: any) =>
            GroupService.deleteUserGroup(id, this.props.groupId).then(
                (group: IGroup) => {

                },
                (error: any) => {
                    if (error && error.response && error.response.data)
                        this.setState({ errorMessage: error.response.data })
                    else if (error.message)
                        this.setState({ errorMessage: error.message })
                }
            )
        )
        alert("operation succesfull");
        this.setState({
            usersToAdd: [],
            usersToAddNames: [],
            usersToDelete: [],
            usersToDeleteNames: [],
            usersToAccept: [],
            usersToAcceptNames: [],
            usersToCancel: [],
            usersToCancelNames: [],
            option: ''
        });
    }

    aprove() {
        this.state.usersToAccept.map((id: any) =>
            // GroupService.deleteUserGroup(id, this.props.groupId).then(
            GroupService.updateUserGroup(id, this.props.groupId, "member").then(
                (group: IGroup) => {

                },
                (error: any) => {
                    if (error && error.response && error.response.data)
                        this.setState({ errorMessage: error.response.data })
                    else if (error.message)
                        this.setState({ errorMessage: error.message })
                }
            )
        )
        alert("operation succesfull");
        this.setState({
            usersToAdd: [],
            usersToAddNames: [],
            usersToDelete: [],
            usersToDeleteNames: [],
            usersToAccept: [],
            usersToAcceptNames: [],
            usersToCancel: [],
            usersToCancelNames: [],
            option: ''
        });
    }

    cancel() {
        this.state.usersToCancel.map((id: any) =>
            GroupService.deleteUserGroup(id, this.props.groupId).then(
                (group: IGroup) => {

                },
                (error: any) => {
                    if (error && error.response && error.response.data)
                        this.setState({ errorMessage: error.response.data })
                    else if (error.message)
                        this.setState({ errorMessage: error.message })
                }
            )
        );
        alert("operation succesfull");
        this.setState({
            usersToAdd: [],
            usersToAddNames: [],
            usersToDelete: [],
            usersToDeleteNames: [],
            usersToAccept: [],
            usersToAcceptNames: [],
            usersToCancel: [],
            usersToCancelNames: [],
            option: ''
        });
        this.forceUpdate();
    }

    componentWillReceiveProps(newProps: any) {
        this.forceUpdate();
    }

    render() {
        return (
            <div>
                <h2> User management </h2>
                <div className="container">
                    <button type="button" className="btn btn-success" onClick={() => this.setOption("addusers")}> Invite users to group</button>
                    <button type="button" className="btn btn-success" onClick={() => this.setOption("deleteusers")}> Delete users from group</button>
                    <button type="button" className="btn btn-success" onClick={() => this.setOption("manage")}> Join requests received</button>
                    <button type="button" className="btn btn-success" onClick={() => this.setOption("invited")}> Invited friends </button>


                    {this.state.option === "addusers" ?
                        <div>
                            <div className="row">
                                <div className="col-sm-10">
                                    <p> {this.state.errorMessage} </p>

                                    <div>
                                        <h3> Select users to invite to group: </h3>
                                        <FriendsToInviteList groupId={this.props.groupId} displayButtons={true} onClickHandler={this.selectUserToInvite} />
                                    </div>
                                </div>
                                <div className="col-sm-2">
                                    <h3>Selected users:</h3>
                                    {this.state.usersToAddNames.map(this.renderNames)}
                                </div>
                                <button className="btn btn-primary" onClick={this.invite}>Invite</button>
                            </div>
                        </div>
                        :
                        this.state.option === "deleteusers" ?
                            <div>
                                <div className="row">
                                    <div className="col-sm-10">
                                        <p> {this.state.errorMessage} </p>

                                        <div>
                                            <h3> Select users to delete from group: </h3>
                                            <MembersToDeleteList groupId={this.props.groupId} displayButtons={true} onClickHandler={this.selectUserToDelete} />
                                        </div>
                                    </div>
                                    <div className="col-sm-2">
                                        <h3>Selected users:</h3>
                                        {this.state.usersToDeleteNames.map(this.renderNames)}
                                    </div>
                                    <button className="btn btn-primary" onClick={this.delete}>Delete</button>
                                </div>
                            </div>
                            :
                            this.state.option === "manage" ?
                                <div>
                                    <div className="row">
                                        <div className="col-sm-10">
                                            <p> {this.state.errorMessage} </p>

                                            <div>
                                                <h3> Select users to aprove join request </h3>
                                                <MembersReqList groupId={this.props.groupId} displayButtons={true} onClickHandler={this.selectUserToAccept} />

                                            </div>
                                        </div>
                                        <div className="col-sm-2">
                                            <h3>Selected users:</h3>
                                            {this.state.usersToAcceptNames.map(this.renderNames)}
                                        </div>
                                        <button className="btn btn-primary" onClick={this.aprove}>Aprove</button>
                                    </div>
                                </div>
                                :
                                this.state.option === "invited" ?
                                    <div>
                                        <div className="row">
                                            <div className="col-sm-10">
                                                <p> {this.state.errorMessage} </p>

                                                <div>
                                                    <h3> Invited users </h3>
                                                    <FriendsInvitedList groupId={this.props.groupId} displayButtons={true} onClickHandler={this.selectUserToCancel} />
                                                </div>
                                            </div>
                                            <div className="col-sm-2">
                                                <h3>Selected users:</h3>
                                                {this.state.usersToCancelNames.map(this.renderNames)}
                                            </div>
                                            <button className="btn btn-primary" onClick={this.cancel}>Cancel</button>
                                        </div>
                                    </div>
                                    :
                                    <div />
                    }
                </div>
            </div >

        );
    }
}