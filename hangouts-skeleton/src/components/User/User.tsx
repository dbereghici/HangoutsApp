import * as React from 'react';
import './User.css';

import { IUser } from './../../models/IUser';

export interface UserProps {
}

interface UserState {
    message: string;
    users: IUser[];
    username: string;
    password: string;
}

export class User extends React.Component<UserProps, UserState> {

    // constructor(props: UserProps) {
    //     super(props);

    //     this.state = {
    //         message: 'This is a message',
    //         users: [],
    //         username: '',
    //         password: ''
    //     };
    // }

    // componentDidMount() {
    //     UsersService.getUsers().then((users) => {
    //         this.setState({
    //             users: users
    //         });
    //     },
    //         (error) => {
    //             this.setState({
    //                 message: "Error"
    //             });
    //         });
    // }

    render() {
        let renderUser = (user: any, index: number) => {
            return (
                <div key={index}>
                    <span>{user.name}</span>
                </div>
            );
        }
        return (
            <div>
                {this.state.users.map(renderUser)}
            </div>
        );
    }
}