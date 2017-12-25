import axios from 'axios';

import { IUser } from './../models/IUser';

export class UsersService {
    private static root: string = 'https://www.anapioficeandfire.com/api/characters/';
    private static startRoot: string = 'http://accesastartapi.azurewebsites.net/api/users';

    public static getUserById(id: number): Promise<IUser> {
        return new Promise((resolve, reject) => {
            axios.get(this.root + id).then((response: any) => {
                let character = this.toUser(response.data);
                resolve(character);
            },
                (error: any) => {
                    reject(error);
                })
        });
    }

    public static getUsers(): Promise<IUser[]> {
        return new Promise((resolve, reject) => {
            axios.get(this.root).then((response: any) => {
                let users = [
                    { name: "Daenerys Targaryen" },
                    { name: "Arya Stark" },
                    { name: "Jon Snow" },
                    { name: "Jamie Lannister" }
                ];
                resolve(users);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static addUser(user: any): Promise<any> {
        return new Promise((resolve, reject) => {
            axios.post(this.startRoot, user).then((response: any) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    private static toUser(responseCharacter: any): IUser {
        return {
            name: responseCharacter.name,
            born: responseCharacter.born,
            gender: responseCharacter.gender
        };
    }
}