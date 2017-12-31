import axios from 'axios';
import { IGroup } from '../models/IGroup';
import { IGroupDataPage } from '../models/IGroupDataPage';
import AuthService from './AuthService';
import { IUser } from '../models/IUser';


export default class GroupService {
    private static groupRoot: string = 'http://localhost:15195/api/group';
    private static userRoot: string = 'http://localhost:15195/api/user';

    public static addGroup(group: any){
        return new Promise<IGroup>((resolve, reject) => {
            axios.post(this.groupRoot, group).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static deleteGroup(groupId: number){
        return new Promise((resolve, reject) => {
            axios.delete(this.groupRoot + "/" + groupId).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static getAllGroupsPage(id: number, q: string, page: number, size: number){
        return new Promise<IGroupDataPage[]>((resolve, reject) => {
            axios.get(this.groupRoot + "/search?id=" + id + "&q=" + q + "&page=" + page + "&size=" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static getMyGroupsPage(q: string, status: string, id: number, page: number, size: number){
        return new Promise<IGroupDataPage>((resolve, reject) => {
            ///111/my/admin/search?q=&page=1&size=6
            axios.get(this.groupRoot + "/" + id + "/my/" + status + "/search?q=" + q + "&page=" + page + "&size=" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static addUserToGroup(userid: number, groupid: number, status: string){
        let usergroup = {
            groupId: groupid,
            userId: userid,
            status: status
        }
        return new Promise((resolve, reject) => {
            axios.post(this.groupRoot + "/user", usergroup).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static updateUserGroup(userid: number, groupid: number, status: string){
        let usergroup = {
            groupId: groupid,
            userId: userid,
            status: status
        }
        return new Promise((resolve, reject) => {
            axios.put(this.groupRoot + "/" + groupid + "/user/" + userid, usergroup).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static getAdministratedGroupsPage(q: string, id: number, page: number, size: number){
        return new Promise<IGroupDataPage>((resolve, reject) => {
            axios.get(this.groupRoot + "administrated/search?id=" + id + "&q=" + q + "&page=" + page + "&size=" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static getGroup(id: number){
        return new Promise<IGroup>((resolve, reject) =>
        {
            axios.get(this.groupRoot + "/" + id + "?userId=" + JSON.parse(AuthService.getUserData()).id).then((response) => {
                resolve(response.data);
            },
            (error: any) => {
                reject(error);
            });
        });
    }

    public static deleteUserGroup(userid: number, groupid: number){
        return new Promise<IGroup>((resolve, reject) => {
            axios.delete(this.groupRoot + "/" + groupid + "/user/" + userid).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static getUsersAvailableToInvite(groupid: number, page:number, size: number){
        return new Promise<IUser[]>((resolve, reject) => {
            //user/availabletoinvite/group?id=70&page=1&size=6
            axios.get(this.userRoot + "/availabletoinvite/group?id=" + groupid + "&page=" + page + "&size=" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static GetUsersWhoAskedToJoinGroup(groupid: number, page:number, size: number){
        return new Promise<IUser[]>((resolve, reject) => {
            //user/availabletoinvite/group?id=70&page=1&size=6
            axios.get(this.userRoot + "/askedtojoin/group?id=" + groupid + "&page=" + page + "&size=" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static GetInvitedUsers(groupid: number, page:number, size: number){
        return new Promise<IUser[]>((resolve, reject) => {
            //user/availabletoinvite/group?id=70&page=1&size=6
            axios.get(this.userRoot + "/invited/group?id=" + groupid + "&page=" + page + "&size=" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }
}