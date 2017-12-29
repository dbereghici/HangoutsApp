import * as React from 'react';
// import 'bootstrap/dist/css/bootstrap.css';
// import 'bootstrap/dist/css/bootstrap-theme.css';
import BaseComponent from '../BaseComponent/BaseComponent';
import GroupService from '../../services/GroupService';
import AuthService from '../../services/AuthService';
import { IGroup } from '../../models/IGroup';
import { AcceptedReqList } from '../Friends/AcceptedReqList';

export class AddNewGroupForm extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.state = {
            groupName: '',
            groupid: 0,
            formValid: false,
            errorMessage: '',
            usersToAdd: [],
            usersToAddNames: []
        }

        this.addGroup = this.addGroup.bind(this);
        this.selectUser = this.selectUser.bind(this);
    }

    inviteUserToGroup(userid: number, groupid: number) {
        GroupService.addUserToGroup(userid, this.state.groupid).then(
            (group: IGroup) => {

            },
            (error: any) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        )
    }

    addGroup(event: any) {
        event.preventDefault();
        let group = {
            adminId: JSON.parse(AuthService.getUserData()).id,
            name: this.state.groupName
        }
        GroupService.addGroup(group).then(
            (group: IGroup) => {
                this.setState({ groupid: group.id });
                this.state.usersToAdd.map((id: any) => this.inviteUserToGroup(id, group.id));
                alert("Operation succesfull");
                this.setState({ errorMessage: '' })
            },
            (error: any) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        )
    }

    handleUserInput(e: any) {
        this.setState({ groupName: e.target.value })
        if (e.target.value === "" || e.target.value.length > 50)
            this.setState({ formValid: false })
        else
            this.setState({ formValid: true })
    }

    selectUser(id: number, name: string) {
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

    renderNames(name: any, index: number) {
        return (
            <div key={index}>
                <p>
                    <b>{name}</b>
                </p>
            </div>
        )
    }

    render() {
        return (
            <div className="row">
                <div className="col-sm-8">
                    <p> {this.state.errorMessage} </p>
                    <form className="demoForm" onSubmit={this.addGroup}>
                        <label> Group name </label>
                        <div className="form-group">
                            <input type="text" className="form-control"
                                name="text" onChange={(event) => this.handleUserInput(event)}
                            />
                        </div >
                        <button type="submit" className="btn btn-primary"
                            disabled={!this.state.formValid}>Add group</button>
                    </form>
                    <div>
                        <h3> Select users : </h3>
                        <AcceptedReqList displayButtons={true} onClickHandler={this.selectUser} />
                    </div>
                </div>
                <div className="col-sm-4">
                    <h3>Selected users:</h3>
                    {this.state.usersToAddNames.map(this.renderNames)}
                </div>
            </div>
        );
    }
}
