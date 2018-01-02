import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
import './Form.css';
import { Redirect } from 'react-router';
import BaseComponent from '../BaseComponent/BaseComponent';
import { Header } from '../Header/Header';
import { EditDataForm } from './EditDataForm';
import { EditPswForm } from './EditPswForm';
import DeleteAccount from './DeleteAccount';

export class MyProfile extends BaseComponent {
    constructor(props: any) {
        super(props);
    }
    render() {
        if (!this.state.isAuth) {
            return <Redirect to='/authentication' />;
        }

        return (
            <div>
                <Header />
                <div className="row">
                    <div className="col-sm-6">
                        <EditDataForm/>
                    </div>
                    <div className="col-sm-6">
                        <EditPswForm/>
                        <DeleteAccount />
                    </div>
                </div>
            </div>
        );
    }
}
