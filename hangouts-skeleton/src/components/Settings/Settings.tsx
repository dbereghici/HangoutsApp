import * as React from 'react';
import './Settings.css';
import AuthService from '../../services/AuthService'
import BaseComponent from '../BaseComponent/BaseComponent';
import { Redirect } from 'react-router';
// import { Link } from 'react-router-dom';

export class Settings extends BaseComponent {

    constructor(props: any) {
        super(props);

        this.logOut = this.logOut.bind(this);
        this.myProfile = this.myProfile.bind(this);

        this.state = {
            isPanelOpened: false,
            redirectToReferrer: ''
        };
    }

    onSettingsHover(event: any) {
        event.preventDefault();
        this.setState({
            isPanelOpened: true
        });
    }

    onSettingsMouseLeave(event: any) {
        event.preventDefault();
        this.setState({
            isPanelOpened: false
        });
    }

    logOut() {
        AuthService.logOut();
        this.setState({ isAuth: false });
        this.setState({ redirectToReferrer: 'authentication', isAuth: true });
    }

    myProfile() {
        this.setState({redirectToReferrer: 'myprofile'})
    }



    render() {
        if (this.state.redirectToReferrer === 'authentication') {
            return <Redirect to='/authentication' />;
        }
        if (this.state.redirectToReferrer === 'myprofile') {
            return <Redirect to='/myprofile' />;
        }
        return (
            <div className="hangouts-settings" onMouseEnter={(event) => { this.onSettingsHover(event) }} onMouseLeave={(event) => { this.onSettingsMouseLeave(event) }}>
                <div className="settings-cog">
                    <i className="fa fa-cog" aria-hidden="true"></i>
                </div>
                {this.state.isPanelOpened ?
                    <ul>
                        <li>
                        <button onClick={this.myProfile}> MyProfile </button>
                        </li>
                        <li>
                        <button> Setting </button>
                        </li>
                        <li>
                        <button onClick={this.logOut}> LogOut </button>
                        </li>
                    </ul>
                    : null}
            </div>
        );
    }
}
