import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
import './Form.css';
import FormErrors from './FormErrors'
import AuthService from '../../services/AuthService'
import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import { AddressSelector } from '../AddressSelector/AddressSelector';
import { Header } from '../Header/Header';


export class MyProfile extends BaseComponent {
    constructor(props: any) {
        super(props);
        this.updateUserData = this.updateUserData.bind(this);
        this.getDataFromMap = this.getDataFromMap.bind(this);
        this.state = {
            ...this.state,
            email: JSON.parse(AuthService.getUserData()).email,
            firstname: JSON.parse(AuthService.getUserData()).firstName,
            lastname: JSON.parse(AuthService.getUserData()).lastName,
            birthdate: JSON.parse(AuthService.getUserData()).birthDate.substr(0, 10),
            address: {
                location: JSON.parse(AuthService.getUserData()).address.location,
                latitude: JSON.parse(AuthService.getUserData()).address.latitude,
                longitude: JSON.parse(AuthService.getUserData()).address.longitude
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
            redirectToReferrer: false
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
        let user = JSON.parse(AuthService.getUserData());
        let userToUpdate = {...user, username : ''};
        userToUpdate.username = this.state.email;
        userToUpdate.email = this.state.email;
        userToUpdate.firstName = this.state.firstname;
        userToUpdate.lastName = this.state.lastname;
        userToUpdate.address = this.state.address;
        userToUpdate.birthdate = this.state.birthdate + "T00:00:00";
        console.log(userToUpdate);
        AuthService.updateUserData(user, user.id).then(

        );
        // console.log(JSON.parse(user));
        
        // let user = {
        //     username : this.state.email,
        //     email: this.state.email,
        //     password: this.state.password,
        //     firstname: this.state.firstname,
        //     lastname: this.state.lastname,
        //     address: this.state.address,
        //     birthdate: this.state.birthdate + "T00:00:00"
        // }
        // AuthService.register(user).then(
        //     (user) => {
        //         this.setState({ redirectToReferrer: true, isAuth: true });
        //     }, 
        //     (error) => {
        //         if(error && error.response && error.response.data)
        //             this.setState({ error: error.response.data });
        //     }
        // )
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
                <Header />
                <div className="row">
                    <div className="col-sm-6">
                        <form className="demoForm" onSubmit={this.updateUserData}>
                            <h2>Edit Account</h2>
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
                                    name="birthdate" value={this.state.birthdate} onChange={(event) => this.handleUserInput(event)} onLoad={(event) => this.handleUserInput(event)}
                                />
                            </div >
                            {<div className="panel panel-default">
                                <FormErrors formErrors={this.state.formErrors} />
                                <p> {this.state.error} </p>
                            </div>
                            }
                            <div className="mapContainer">
                                <label htmlFor="text">Address*</label>
                                <AddressSelector getDataFromMap={this.getDataFromMap} loadUserLocation = {true} userLocation = {{lat : this.state.address.latitude, lng : this.state.address.longitude}} />
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
            </div>
        );
    }
}
