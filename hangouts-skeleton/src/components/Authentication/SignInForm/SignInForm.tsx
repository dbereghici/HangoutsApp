import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
import './Form.css';
import FormErrors from './FormErrors'
import AuthService from '../../../services/AuthService'
import { Redirect } from 'react-router';
import BaseComponent from '../../BaseComponent/BaseComponent';


export class SignInForm extends BaseComponent {

    constructor(props: any) {
        super(props);
        this.authenticate = this.authenticate.bind(this);
        this.state = {
            ...this.state,
            email: '',
            password: '',
            formErrors: { email: '', password: '' },
            emailValid: false,
            passwordValid: false,
            formValid: false,
            error: '',
            redirectToReferrer: false
        }
    }

    validateField(fieldName: any, value: any) {
        let fieldValidationErrors = this.state.formErrors;
        let emailValid = this.state.emailValid;
        let passwordValid = this.state.passwordValid;

        switch (fieldName) {
            case 'email':
                emailValid = value.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i);
                fieldValidationErrors.email = emailValid ? '' : ' is invalid';
                break;
            case 'password':
                passwordValid = value.length >= 6;
                fieldValidationErrors.password = passwordValid ? '' : ' is too short';
                break;
            default:
                break;
        }
        this.setState({
            formErrors: fieldValidationErrors,
            emailValid: emailValid,
            passwordValid: passwordValid
        }, this.validateForm);
    }

    validateForm() {
        this.setState({ formValid: this.state.emailValid && this.state.passwordValid });
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

    authenticate(event: any) {
        event.preventDefault();
        let user = {
            email: this.state.email,
            password: this.state.password
        }
        AuthService.authenticate(user).then(
            (user) => {
                this.setState({ redirectToReferrer: true, isAuth: true });
            },
            (error) => {
                this.setState({ error: error.response.data });

            }
        );
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
                <form className="demoForm" onSubmit={this.authenticate}>
                    <h2>Sign in</h2>
                    <div className="form-group">
                        <label htmlFor="email"  >Email address</label>
                        <input type="email" className="form-control"
                            name="email" value={this.state.email} onChange={(event) => this.handleUserInput(event)}
                        />
                    </div >
                    <div className={`form-group ${this.errorClass(this.state.formErrors.email)}`}>
                        <label htmlFor="password">Password</label>
                        <input type="password" className="form-control"
                            name="password" value={this.state.password} onChange={(event) => this.handleUserInput(event)}
                        />
                    </div >
                    <div className="panel panel-default">
                        <FormErrors formErrors={this.state.formErrors} />
                        <p> {this.state.error} </p>
                    </div>
                    <button type="submit" className="btn btn-primary"
                        disabled={!this.state.formValid}>Sign up</button>
                </form >
            </div>
        );
    }
}
