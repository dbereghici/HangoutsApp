import axios from 'axios';


export default class AuthService {
    private static userStorageKey : string = 'user';
    private static startRoot: string = 'http://localhost:15195/api/authentication';
    private static userRoot: string = 'http://localhost:15195/api/user';

    public static isAuth(){
        let user = localStorage.getItem(this.userStorageKey);
        return !!user;
    }

    public static authenticate(user : any){
        return new Promise((resolve, reject) => {
            axios.post(this.startRoot, user).then((response: any) => {
                localStorage.setItem(this.userStorageKey, JSON.stringify(response.data));
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static register(user : any){
        return new Promise((resolve, reject) => {
            axios.post(this.userRoot, user).then((response: any) => {
                // let dataToBeStored = JSON.stringify(response.data);
                // console.log(dataToBeStored);
                localStorage.setItem(this.userStorageKey, JSON.stringify(response.data));
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static updateUserData(user : any, id : number){
        return new Promise((resolve, reject) => {
            axios.put(this.userRoot + "/" + id, user).then((response: any) => {
                // let dataToBeStored = JSON.stringify(response.data);
                // debugger;
                // console.log(dataToBeStored);
                localStorage.setItem(this.userStorageKey, JSON.stringify(response.data));
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static logOut(){
        localStorage.removeItem(this.userStorageKey);
    }

    public static getUserData() : any{
        // let userData = localStorage.getItem(this.userStorageKey);
        // console.log(userData);
        return localStorage.getItem(this.userStorageKey);
    }
}