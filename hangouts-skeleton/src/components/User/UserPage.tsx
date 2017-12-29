import * as React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
import BaseComponent from '../BaseComponent/BaseComponent';
import { Header } from '../Header/Header';
import { UsersList } from './UserList';
import { UsersService } from '../../services/UsersService';

export class UserPage extends BaseComponent {
    constructor(props: any) {
        super(props);
        this.getUsers = this.getUsers.bind(this);
    }

    getUsers(id: number, page: number, size: number){
        UsersService.GetAllUsersWithRelationStatusPage(id, page, size);
    }

    render() {
        return (
            <div>
                <Header />
                <UsersList getUsers={this.getUsers}/>
            </div>
        )
    }
}
