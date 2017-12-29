import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
import BaseComponent from '../BaseComponent/BaseComponent';
import { Header } from '../Header/Header';
import GroupService from '../../services/GroupService';
import { UsersService } from '../../services/UsersService';
import { User } from '../User/User';

export class Group extends BaseComponent {
    constructor(props: any) {
        super(props);

        this.state = {
            errorMessage: '',
            admin: {
                id : 0,
                username : '',
                firstName : '',
                lastName : '',
                age : 0,
                address : '',
                relationshipStatus : ''
            },
            group: {
                id: 0,
                admin: '',
                adminId: 0,
                Name: '',
                nrOfMembers: 0
            }
        }
    }

    componentDidMount() {
        GroupService.getGroup(this.props.match.params.id).then(
            (group) => {
                this.setState({
                    group: group,
                    errorMessage: ''
                });
                group.adminID;
                UsersService.getUser(group.adminID).then(
                    (user) => {
                        this.setState({
                            admin: user
                        })
                    }, 
                    (error) => {
                        if (error && error.response && error.response.data)
                            this.setState({ errorMessage: error.response.data })
                        else if (error.message)
                            this.setState({ errorMessage: error.message })
                    }
                );
            },
            (error) => {
                if (error && error.response && error.response.data)
                    this.setState({ errorMessage: error.response.data })
                else if (error.message)
                    this.setState({ errorMessage: error.message })
            }
        );
    }

    render() {
        return (
            <div>
                <Header />
                <p> {this.state.errorMessage} </p>
                {/* <p> Group ID => {this.props.match.params.id} </p> */}

                <div className="container-fluid text-center">
                    <div className="row content">
                        <div className="col-sm-2 sidenav">
                            <h2><b>Admin:</b></h2>
                            <User 
                                    UserData={this.state.admin} 
                                />


                        </div>
                        <div className="col-sm-8 text-left">
                            <h2> {this.state.group.name} </h2>
                            <p> <b>  {this.state.group.nrOfMembers} members</b> </p>
                            <hr />
                            <h3>Test</h3>
                            <p>Lorem ipsum...</p>
                        </div>
                        <div className="col-sm-2 sidenav">
                            <div className="well">
                                <p>{this.state.group.status}</p>
                            </div>
                            <div className="well">
                                <p>ADS</p>
                            </div>
                        </div>
                    </div>
                </div>

                <footer className="container-fluid text-center">
                    <p>Footer Text</p>
                </footer>



            </div >
        );
    }
}
