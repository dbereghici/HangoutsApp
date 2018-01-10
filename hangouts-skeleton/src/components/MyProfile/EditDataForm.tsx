import * as React from 'react';
//import 'bootstrap/dist/css/bootstrap.css';
//import 'bootstrap/dist/css/bootstrap-theme.css';
import './Form.css';
import FormErrors from './FormErrors'
import AuthService from '../../services/AuthService'
import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import { AddressSelector } from '../AddressSelector/AddressSelector';


export class EditDataForm extends BaseComponent {
    constructor(props: any) {
        super(props);
        this.updateUserData = this.updateUserData.bind(this);
        this.getDataFromMap = this.getDataFromMap.bind(this);
        let authUser = JSON.parse(AuthService.getUserData());
        this.state = {
            ...this.state,
            email: authUser.email,
            username: authUser.username,
            firstname: authUser.firstName,
            lastname: authUser.lastName,
            birthdate: authUser.birthDate,
            address: {
                location: authUser.address.location,
                latitude: authUser.address.latitude,
                longitude: authUser.address.longitude
            },
            formErrors: {
                email: '',
                firstname: '',
                lastname: '',
                birthdate: new Date(),
                address: {
                    location: '',
                    latitude: 0,
                    longitude: 0
                },
            },
            emailValid: true,
            firstnameValid: true,
            lastnameValid: true,
            birthdateValid: true,
            addressValid: true,
            formValid: true,
            error: '',
            redirectToReferrer: false,
            authUser: JSON.parse(AuthService.getUserData())
        }
    }


    validateField(fieldName: any, value: any) {
        let fieldValidationErrors = this.state.formErrors;
        let emailValid = this.state.emailValid;
        let firstnameValid = this.state.firstnameValid;
        let lastnameValid = this.state.lastnameValid;
        let birthdateValid = this.state.birthdateValid;

        switch (fieldName) {
            case 'email':
                emailValid = value.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i);
                fieldValidationErrors.email = emailValid ? '' : ' is invalid';
                break;
            case 'firstname':
                firstnameValid = value.length >= 1;
                fieldValidationErrors.firstname = firstnameValid ? '' : ' must be filled in';
                break;
            case 'lastname':
                lastnameValid = value.length >= 1;
                fieldValidationErrors.lastname = lastnameValid ? '' : ' must be filled in';
                break;
            case 'birthdate':
                birthdateValid = isNaN(value);
                fieldValidationErrors.birthdate = birthdateValid ? '' : ' must be filled in';
                break;
            default:
                break;
        }
        this.setState({
            formErrors: fieldValidationErrors,
            emailValid: emailValid,
            firstnameValid: firstnameValid,
            lastnameValid: lastnameValid,
            birthdateValid: birthdateValid
        }, this.validateForm);
    }

    validateForm() {
        this.setState({
            formValid: this.state.emailValid && this.state.lastnameValid
                && this.state.firstnameValid && this.state.birthdateValid
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
        let user = this.state.authUser;
        let userToUpdate = { ...user, username: '' };
        userToUpdate.username = this.state.username;
        userToUpdate.email = this.state.email;
        userToUpdate.firstName = this.state.firstname;
        userToUpdate.lastName = this.state.lastname;
        // if(userToUpdate.adress)
        userToUpdate.address = this.state.address;
        userToUpdate.birthDate = this.state.birthdate;
        AuthService.updateUserData(userToUpdate, user.id).then(
            (user) => {
                this.setState({
                    email: this.state.authUser.email,
                    firstname: this.state.authUser.firstName,
                    lastname: this.state.authUser.lastName,
                    birthdate: this.state.authUser.birthDate,
                    address: {
                        location: this.state.authUser.address.location,
                        latitude: this.state.authUser.address.latitude,
                        longitude: this.state.authUser.address.longitude
                    },
                });
                alert("Operation successfull");
            },
            (error) => {
                if (error && error.response && error.response.data)
                    alert(error.response.data);    

            }
        );
    }

    getDataFromMap(address: any) {
        this.setState({ address: address })
    }

    render() {
        if (!this.state.isAuth) {
            return <Redirect to='/authentication' />;
        }
        return (
            <div>
                <div>
                        <form className="demoForm" onSubmit={this.updateUserData}>
                            <h2>Edit Account</h2>
                            <div className="form-group">
                                <label htmlFor="username"  >Username</label>
                                <input type="text" className="form-control"
                                    name="username" value={this.state.username} disabled
                                />
                            </div>
                            <div className="form-group">
                                <label htmlFor="email"  >Email address*</label>
                                <input type="email" className="form-control"
                                    name="email" value={this.state.email} onChange={(event) => this.handleUserInput(event)} onLoad={(event) => this.handleUserInput(event)}
                                />
                            </div >
                            <div>
                                <label htmlFor="text">First name*</label>
                                <input type="text" className="form-control"
                                    name="firstname" value={this.state.firstname} onChange={(event) => this.handleUserInput(event)} onLoad={(event) => this.handleUserInput(event)}
                                />
                            </div >
                            <div>
                                <label htmlFor="text">Last name*</label>
                                <input type="text" className="form-control"
                                    name="lastname" value={this.state.lastname} onChange={(event) => this.handleUserInput(event)} onLoad={(event) => this.handleUserInput(event)}
                                />
                            </div >
                            <div>
                                <label htmlFor="text">Birthdate*</label>
                                <input type="date" className="form-control"
                                    name="birthdate" value={this.state.birthdate.substr(0, 10)} onChange={(event) => this.handleUserInput(event)} onLoad={(event) => this.handleUserInput(event)}
                                />
                            </div >
                            {<div className="panel panel-default">
                                <FormErrors formErrors={this.state.formErrors} />
                                <p> {this.state.error} </p>
                            </div>
                            }
                            <div className="mapContainer">
                                <label htmlFor="text">Address*</label>
                                <AddressSelector getDataFromMap={this.getDataFromMap} loadUserLocation={true} userLocation={{ lat: this.state.address.latitude, lng: this.state.address.longitude }} />
                            </div >
                            <button type="submit" className="btn btn-primary"
                                disabled={!this.state.formValid}>Edit</button>
                        </form >
                    </div>

                    {/* Password form */}
                    {/* <div className="col-sm-6">
                        <form className="demoForm" onSubmit={this.register}>
                            <h2>Change Password</h2>
                            <div className="form-group">
                                <label htmlFor="email"  >Old Password*</label>
                                <input type="email" className="form-control"
                                    name="email" value={this.state.email} onChange={(event) => this.handleUserInput(event)} onLoad={(event) => this.handleUserInput(event)}
                                />
                            </div >
                            <div className={`form-group ${this.errorClass(this.state.formErrors.email)}`}>
                                <label htmlFor="password">New Password*</label>
                                <input type="password" className="form-control"
                                    name="password" value={this.state.password} onChange={(event) => this.handleUserInput(event)} onLoad={(event) => this.handleUserInput(event)}
                                />
                            </div >

                            <button type="submit" className="btn btn-primary"
                                disabled={!this.state.formValid}>Change</button>
                        </form >
                    </div> */}
                </div>
        );
    }
}
