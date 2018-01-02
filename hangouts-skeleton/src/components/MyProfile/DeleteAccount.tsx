import * as React from 'react';
import BaseComponent from '../BaseComponent/BaseComponent';
import { Modal, Button } from 'react-bootstrap';
import { Redirect } from 'react-router';
import AuthService from '../../services/AuthService';
import { UsersService } from '../../services/UsersService';

export default class DeleteAccount extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.close = this.close.bind(this);
        this.open = this.open.bind(this);
        this.delete = this.delete.bind(this);

        this.state = {
            showModal: false
        }
    }


    close() {
        this.setState({ showModal: false });
    }

    open() {
        this.setState({ showModal: true });
    }

    delete() {
        UsersService.DeleteUser(JSON.parse(AuthService.getUserData()).id).then(
            () => {
                AuthService.logOut();
                this.setState({ isAuth: false });
                this.setState({ redirectToReferrer: 'authentication', isAuth: true });
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        )
    }


    render() {
        if (this.state.redirectToReferrer === 'authentication') {
            return <Redirect to='/authentication' />;
        }
        return (
            <div className="demoForm">
                <h2> Delete account </h2>
                <p> {this.state.errorMessage} </p>
                <Button
                    bsStyle="primary"
                    // bsSize="large"
                    onClick={this.open}
                >
                    Delete
                </Button>

                <Modal show={this.state.showModal} onHide={this.close}>
                    <Modal.Header closeButton>
                        <Modal.Title>Delete Account</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <p> Are you sure you want to delete your account?</p>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button onClick={this.delete}>Yes</Button>
                        <Button onClick={this.close}>No</Button>
                    </Modal.Footer>
                </Modal>
            </div>
        );
    }
}