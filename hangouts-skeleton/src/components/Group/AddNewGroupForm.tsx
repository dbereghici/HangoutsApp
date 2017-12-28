import * as React from 'react';
// import 'bootstrap/dist/css/bootstrap.css';
// import 'bootstrap/dist/css/bootstrap-theme.css';
import BaseComponent from '../BaseComponent/BaseComponent';
import GroupService from '../../services/GroupService';
import AuthService from '../../services/AuthService';
import { IGroup } from '../../models/IGroup';

export class AddNewGroupForm extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.state = {
            groupName: '',
            formValid: false
        }

        this.addGroup = this.addGroup.bind(this);
    }

    addGroup(event: any) {
        event.preventDefault();
        let group = {
            adminId : JSON.parse(AuthService.getUserData()).id,
            name : this.state.groupName
        }
        GroupService.addGroup(group).then(
            (group: IGroup) => {
                alert("Operation succesfull");
                this.setState({errorMessage: ''})
            },
            (error: any) =>{
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

    render() {
        return (
            <div>
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
            </div>
        );
    }
}
