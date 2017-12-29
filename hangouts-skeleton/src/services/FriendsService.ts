import axios from 'axios';
import { IUser } from '../models/IUser';
import { IUserDataPage } from '../models/IUserDataPage';

export default class FriendService {
    private static friendsRoot: string = 'http://localhost:15195/api/friends/user';
    private static friendRequestMadeRoot = 'http://localhost:15195/api/friendrequestmade/user'; 
    private static friendRequestReceivedRoot = 'http://localhost:15195/api/friendrequestreceived/user';
    private static friendshipRoot = 'http://localhost:15195/api/friendship';
    private static friendsSearch = 'http://localhost:15195/api/user'; 

    public static getFriends(id : number){
        return new Promise<IUser[]>((resolve, reject) => {
            axios.get(this.friendsRoot + "/" + id).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static getFriendsPage(id: number, page: number, size: number){
        return new Promise<IUserDataPage>((resolve, reject) => {
            axios.get(this.friendsRoot + "/" + id + "/page/" + page + "/size/" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static getFriendRequestMadePage(id: number, page: number, size: number){
        return new Promise<IUserDataPage>((resolve, reject) => {
            axios.get(this.friendRequestMadeRoot + "/" + id + "/page/" + page + "/size/" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static getFriendRequestReceivedPage(id: number, page: number, size: number){
        return new Promise<IUserDataPage>((resolve, reject) => {
            axios.get(this.friendRequestReceivedRoot + "/" + id + "/page/" + page + "/size/" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static deleteFriendship(id1: number, id2: number){
        return new Promise<IUserDataPage>((resolve, reject) => {
            axios.delete(this.friendshipRoot + "/" + id1 + "/" + id2).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static addFriendship(id1: number, id2: number){
        let friendshipObj = {userId1: id1, userId2: id2, status: "pending"};
        return new Promise((resolve, reject) => {
            axios.post(this.friendshipRoot, friendshipObj).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }
    
    public static updateFriendship(id1: number, id2: number, status: string){
        let statusObj = {status: status};
        return new Promise((resolve, reject) => {
            axios.put(this.friendshipRoot + "/" + id1 + "/" + id2, statusObj).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static getNewFriendsSearchPage(id: number, q : string,  page: number, size: number){
        return new Promise<IUserDataPage>((resolve, reject) => {
            axios.get(this.friendsSearch + "/" + id + "/friends/search?q=" + q + "&page=" + page + "&size=" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

}