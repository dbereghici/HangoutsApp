import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
import './Form.css';
import FormErrors from './FormErrors'
import AuthService from '../../../services/AuthService'
import { Redirect } from 'react-router';
import BaseComponent from '../../BaseComponent/BaseComponent';
import { AddressSelector } from '../../AddressSelector/AddressSelector';


export class RegistrationForm extends BaseComponent {

    constructor(props: any) {
        super(props);
        this.register = this.register.bind(this);
        this.getDataFromMap = this.getDataFromMap.bind(this);
        this.state = {
            ...this.state,
            email: '',
            username: '',
            password: '',
            confirmpassword: '',
            firstname: '',
            lastname: '',
            birthdate: new Date(),
            address: {
                location: '',
                latitude: 0,
                longitude: 0
            },
            formErrors: { 
                email: '', 
                username: '',
                password: '', 
                confirmpassword: '',
                firstname: '', 
                lastname: '', 
                birthdate: new Date(),
                address: {
                    location: '',
                    latitude: 0,
                    longitude: 0
                },
            },
            emailValid: false,
            usernameValid: false,
            passwordValid: false,
            confirmpasswordValid : false,
            firstnameValid : false,
            lastnameValid : false,
            birthdateValid : false,
            addressValid : false,
            formValid: false,
            error: '',
            redirectToReferrer: false
        }
    }

    validateField(fieldName: any, value: any) {
        let fieldValidationErrors = this.state.formErrors;
        let usernameValid = this.state.usernameValid;
        let emailValid = this.state.emailValid;
        let passwordValid = this.state.passwordValid;
        let confirmpasswordValid = this.state.confirmpasswordValid;
        let firstnameValid = this.state.firstnameValid;
        let lastnameValid = this.state.lastnameValid;
        let birthdateValid = this.state.birthdateValid;

        switch (fieldName) {
            case 'email':
                emailValid = value.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i);
                fieldValidationErrors.email = emailValid ? '' : ' is invalid';
                break;
            case 'username':
                usernameValid = (value.length >= 6);
                fieldValidationErrors.username = usernameValid ? '' : ' is too short';
                break;
            case 'password':
                passwordValid = value.length >= 6;
                fieldValidationErrors.password = passwordValid ? '' : ' is too short';
                break;
            case 'confirmpassword':
                confirmpasswordValid = this.state.password === this.state.confirmpassword;
                fieldValidationErrors.confirmpassword = confirmpasswordValid ? '' : "doesn't match with password";
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
                birthdateValid = isNaN(value) && (new Date (value) < new Date(Date.now()));
                fieldValidationErrors.birthdate = birthdateValid ? '' : ' must be lower than the current date';
                break;
            default:
                break;
        }
        this.setState({
            formErrors: fieldValidationErrors,
            emailValid: emailValid,
            usernameValid: usernameValid,
            passwordValid: passwordValid,
            confirmpasswordValid: confirmpasswordValid,
            firstnameValid : firstnameValid,
            lastnameValid : lastnameValid,
            birthdateValid : birthdateValid
        }, this.validateForm);
    }

    validateForm() {
        this.setState({ formValid: this.state.emailValid && this.state.passwordValid && this.state.lastnameValid && this.state.usernameValid
            && this.state.firstnameValid && this.state.birthdateValid && this.state.confirmpasswordValid});
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

    register(event: any) {
        event.preventDefault();
        let user = {
            username : this.state.username,
            email: this.state.email,
            password: this.state.password,
            firstname: this.state.firstname,
            lastname: this.state.lastname,
            address: this.state.address,
            birthdate: this.state.birthdate + "T00:00:00"
        }
        AuthService.register(user).then(
            (user) => {
                this.setState({ redirectToReferrer: true, isAuth: true });
            }, 
            (error) => {
                if(error.message)
                    this.setState({ error: error.message });
                if(error && error.response && error.response.data)
                    this.setState({ error: error.response.data });
            }
        )
        // AuthService.authenticate(user).then(
        //     (user) => {
        //         this.setState({ redirectToReferrer: true, isAuth: true });
        //     },
        //     (error) => {
        //         this.setState({ error: error.response.data });

        //     }
        // );
    }

    getDataFromMap(address : any){
        this.setState({address: address})
        // let user = {
        //     email: this.state.email,
        //     password: this.state.password,
        //     firstname: this.state.firstname,
        //     lastname: this.state.lastname,
        //     birthdate: this.state.birthdate,
        //     address: this.state.address
        // }
        // console.log(user)
    }

    render() {
        if (this.state.isAuth) {
            return <Redirect to='/home' />;
        }
        if (this.state.redirectToReferrer) {
            return <Redirect to='/home' />;
        }
        return (
            <div>
                <form className="demoForm" onSubmit={this.register}>
                    <h2>Register</h2>
                    <div>
                        <label htmlFor="text">Username*</label>
                        <input type="text" className="form-control"
                            name="username" value={this.state.username} onChange={(event) => this.handleUserInput(event)} onSelect={(event) => this.handleUserInput(event)}
                        />
                    </div >
                    <div className="form-group">
                        <label htmlFor="email"  >Email address*</label>
                        <input type="email" className="form-control"
                            name="email" value={this.state.email} onChange={(event) => this.handleUserInput(event)} onSelect={(event) => this.handleUserInput(event)}
                        />
                    </div >
                    <div className={`form-group ${this.errorClass(this.state.formErrors.email)}`}>
                        <label htmlFor="password">Password*</label>
                        <input type="password" className="form-control"
                            name="password" value={this.state.password} onChange={(event) => this.handleUserInput(event)} onSelect={(event) => this.handleUserInput(event)}
                        />
                    </div >
                    <div className={`form-group ${this.errorClass(this.state.formErrors.email)}`}>
                        <label htmlFor="confirmpassword">Confirm Password*</label>
                        <input type="password" className="form-control"
                            name="confirmpassword" value={this.state.confirmpassword} onChange={(event) => this.handleUserInput(event)} onSelect={(event) => this.handleUserInput(event)}
                        />
                    </div >
                    <div>
                        <label htmlFor="text">First name*</label>
                        <input type="text" className="form-control"
                            name="firstname" value={this.state.firstname} onChange={(event) => this.handleUserInput(event)} onSelect={(event) => this.handleUserInput(event)}
                        />
                    </div >
                    <div>
                        <label htmlFor="text">Last name*</label>
                        <input type="text" className="form-control"
                            name="lastname" value={this.state.lastname} onChange={(event) => this.handleUserInput(event)} onSelect={(event) => this.handleUserInput(event)}
                        />
                    </div >
                    <div>
                        <label htmlFor="text">Birthdate*</label>
                        <input type="date" className="form-control"
                            name="birthdate" onChange={(event) => this.handleUserInput(event)} onSelect={(event) => this.handleUserInput(event)}
                        />
                    </div >
                    {<div className="panel panel-default">
                        <FormErrors formErrors={this.state.formErrors} />
                        <p> {this.state.error} </p>
                    </div>
                    }
                    <div className="mapContainer">
                        <label htmlFor="text">Address*</label>
                        <AddressSelector getDataFromMap={this.getDataFromMap}/>
                    </div >
                    <button type="submit" className="btn btn-primary"
                        disabled={!this.state.formValid}>Register</button>
                </form >
            </div>
        );
    }
}
