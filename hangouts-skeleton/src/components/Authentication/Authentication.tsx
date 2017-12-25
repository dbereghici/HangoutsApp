import * as React from 'react';
import { SignInForm } from './SignInForm/SignInForm';
import { RegistrationForm } from './RegistrationForm/Registration';
import './Authentication.css'

export class Authentication extends React.Component<any, any> {

    constructor(props: any) {
        super(props);
    }

    render() {

        return (
            <div>
                <div className="row">
                    <div className="col-sm-6">
                        <SignInForm/>                
                    </div>
                    <div className="col-sm-6">
                        <RegistrationForm/>
                    </div>
                </div>
                {/* <ul className="nav nav-tabs">
                    <li className="active"><a data-toggle="tab" href="#signin">Sign In</a></li>
                    <li><a data-toggle="tab" href="#register">Register </a></li>
                </ul>

                <div className="tab-content">
                    <div id="signin" className="tab-pane fade in active">
                        <SignUpForm/>                
                    </div>
                    <div id="register" className="tab-pane fade">
                        <RegistrationForm/>
                    </div>
                </div> */}
            </div>
        );
    }
}
