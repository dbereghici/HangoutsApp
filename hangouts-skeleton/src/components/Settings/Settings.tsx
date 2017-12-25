import * as React from 'react';
import './Settings.css';
import AuthService from '../../services/AuthService'
import BaseComponent from '../BaseComponent/BaseComponent';
import { Redirect } from 'react-router';

export class Settings extends BaseComponent {

    constructor(props: any) {
        super(props);

        this.logOut = this.logOut.bind(this);

        this.state = {
            isPanelOpened: false,
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
        this.setState({ redirectToReferrer: true, isAuth: true });
    }

    render() {
        if (this.state.redirectToReferrer) {
            return <Redirect to='/authentication' />;
        }
        return (
            <div className="hangouts-settings" onMouseEnter={(event) => { this.onSettingsHover(event) }} onMouseLeave={(event) => { this.onSettingsMouseLeave(event) }}>
                <div className="settings-cog">
                    <i className="fa fa-cog" aria-hidden="true"></i>
                </div>
                {this.state.isPanelOpened ?
                    <ul>
                        <li>
                        <button> My Profile </button>
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
