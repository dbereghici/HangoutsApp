import * as React from 'react';
// import 'bootstrap/dist/css/bootstrap.css';
// import 'bootstrap/dist/css/bootstrap-theme.css';
import './Form.css';
import AuthService from '../../services/AuthService'
import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import FormErrors from './FormErrors';


export class EditPswForm extends BaseComponent {
    constructor(props: any) {
        super(props);
        this.updateUserData = this.updateUserData.bind(this);
        this.getDataFromMap = this.getDataFromMap.bind(this);
        this.state = {
            ...this.state,
            oldpassword: '',
            password: '',
            confirmpassword: '',
            formErrors: {
                oldpassword: '',
                password: '',
                confirmpassword: ''
            },
            oldpasswordValid: false,
            passwordValid: false,
            confirmpasswordValid: false,
            formValid: false,
            error: '',
            redirectToReferrer: false
        }
    }

    validateField(fieldName: any, value: any) {
        let fieldValidationErrors = this.state.formErrors;
        let passwordValid = this.state.passwordValid;
        let confirmpasswordValid = this.state.confirmpasswordValid;

        switch (fieldName) {
            case 'password':
                passwordValid = value.length >= 6;
                fieldValidationErrors.password = passwordValid ? '' : ' is too short';
                break;
            case 'confirmpassword':
                confirmpasswordValid = this.state.password === this.state.confirmpassword;
                fieldValidationErrors.confirmpassword = confirmpasswordValid ? '' : " doesn't match with password";
                break;
            default:
                break;
        }
        this.setState({
            formErrors: fieldValidationErrors,
            passwordValid: passwordValid,
            confirmpasswordValid: confirmpasswordValid
        }, this.validateForm);
    }

    validateForm() {
        this.setState({
            formValid: this.state.passwordValid && this.state.confirmpasswordValid
        });
    }

    handleUserInput(e: any) {
        const name = e.target.name;
        const value = e.target.value;
        this.setState({ [name]: value },
            () => { this.validateField(name, value) });
    }

    errorClass(error: any) {
        return (error.length === 0 ? '' : 'has-error');
    }

    updateUserData(event: any) {
        event.preventDefault();
        let oldpasswordValid = this.state.oldpasswordValid;
        let fieldValidationErrors = this.state.formErrors;
        
        oldpasswordValid = this.state.oldpassword === JSON.parse(AuthService.getUserData()).password;
        fieldValidationErrors.oldpassword = oldpasswordValid ? '' : " is wrong";

        this.setState({
            formErrors: fieldValidationErrors,
            oldpasswordValid: oldpasswordValid
        }, this.validateForm);

        if(oldpasswordValid){
            let user = JSON.parse(AuthService.getUserData());
            let userToUpdate = { ...user};
            userToUpdate.password = this.state.password;
            AuthService.updateUserData(userToUpdate, user.id).then(
                (user) => {
                    this.setState({
                        password: '',
                        oldpassword: '',
                        confirmpassword: ''
                    });
                    // this.setState({error: "The password was modified with succes"})
                    alert("Operation successfull");
                },
                (error) => {
                    if (error && error.response && error.response.data)
                        alert(error.response.data);
                        // this.setState({ error: error.response.data });

                }
            );
        } else{
            this.setState({error: ""})
        }
    }

    getDataFromMap(address: any) {
        this.setState({ address: address })
    }

    render() {
        if (!this.state.isAuth) {
            return <Redirect to='/authentication' />;
        }
        // if (this.state.redirectToReferrer) {
        //     return <Redirect to='/home' />;
        // }
        return (
            <div>
                {/* Password form */}
                <form className="demoForm" onSubmit={this.updateUserData}>
                    <h2>Change Password</h2>
                    <div className="form-group">
                        <label htmlFor="oldpassword"  >Old Password*</label>
                        <input type="password" className="form-control"
                            name="oldpassword" onChange={(event) => this.handleUserInput(event)} 
                        />
                    </div >
                    <div className="form-group">
                        <label htmlFor="password">New Password*</label>
                        <input type="password" className="form-control"
                            name="password" onChange={(event) => this.handleUserInput(event)} onSelect={(event) => this.handleUserInput(event)}
                        />
                    </div >
                    <div className="form-group">
                        <label htmlFor="confirmpassword">Confirm New Password*</label>
                        <input type="password" className="form-control"
                            name="confirmpassword" onChange={(event) => this.handleUserInput(event)} onSelect={(event) => this.handleUserInput(event)}
                        />
                    </div >
                    <div className="panel panel-default">
                                <FormErrors formErrors={this.state.formErrors} />
                                <p> {this.state.error} </p>
                            </div>
                            


                    <button type="submit" className="btn btn-primary"
                        disabled={!this.state.formValid}>Change</button>
                </form >
            </div>
        );
    }
}
