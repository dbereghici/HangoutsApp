import axios from 'axios';
import { IGroup } from '../models/IGroup';
import { IGroupDataPage } from '../models/IGroupDataPage';


export default class GroupService {
    private static groupRoot: string = 'http://localhost:15195/api/group';

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

    public static getAllGroupsPage(q: string, page: number, size: number){
        return new Promise<IGroupDataPage>((resolve, reject) => {
            axios.get(this.groupRoot + "/search?q=" + q + "&page=" + page + "&size=" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static getMyGroupsPage(q: string, id: number, page: number, size: number){
        return new Promise<IGroupDataPage>((resolve, reject) => {
            axios.get(this.groupRoot + "my/search?id=" + id + "&q=" + q + "&page=" + page + "&size=" + size).then((response) => {
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
}