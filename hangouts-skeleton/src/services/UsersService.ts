import axios from 'axios';
import { IUserDataPage } from '../models/IUserDataPage';
import { IUser } from '../models/IUser';

export class UsersService {
    private static userRoot: string = 'http://localhost:15195/api/user';
    private static friendshipRoot: string = 'http://localhost:15195/api/friendship';

    public static GetAllUsersWithRelationStatusPage(id: number, page: number, size: number) {
        return new Promise<IUserDataPage>((resolve, reject) => {
            axios.get(this.userRoot + "/" + id + "/page/" + page + "/size/" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static GetFriendshipStatus(idAuthUser: number, idUser: number) {
        return new Promise((resolve, reject) => {
            axios.get(this.friendshipRoot + "/status?userAuthId=" + idAuthUser + "&userId=" + idUser).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static DeleteUser(id: number){
        return new Promise((resolve, reject) => {
            axios.delete(this.userRoot + "/" + id).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        }); 
    }


    public static getAllUsersSearchPage(id: number, q: string, page: number, size: number) {
        return new Promise<IUserDataPage>((resolve, reject) => {
            axios.get(this.userRoot + "/" + id + "/search?q=" + q + "&page=" + page + "&size=" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static getUser(id: number) {
        return new Promise<IUser>((resolve, reject) => {
            axios.get(this.userRoot + "/" + id).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    
    public static getAllUsersFromGroup(groupid: number, userid: number, q: string, page: number, size: number){
        return new Promise<IUserDataPage[]>((resolve, reject) => {
            axios.get(this.userRoot + "/group/" + groupid + "/search?userId=" + userid + "&q=" + q + "&page=" + page + "&size=" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static getAllUsersFromPlan(planid: number, userid: number, q: string, page: number, size: number){
        return new Promise<IUserDataPage[]>((resolve, reject) => {
            axios.get(this.userRoot + "/plan/" + planid + "/search?userId=" + userid + "&q=" + q + "&page=" + page + "&size=" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }
}