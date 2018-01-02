import axios from 'axios';
import { IPlan } from '../models/IPlan';
import AuthService from './AuthService';
import { IUser } from '../models/IUser';

export class PlanService {
    private static planRoot: string = 'http://localhost:15195/api/plan';
    private static userRoot: string = 'http://localhost:15195/api/user';

    public static GetAllPlansPage(userId: number, groupId: number, page: number, size: number) {
        return new Promise<IPlan[]>((resolve, reject) => {
            axios.get(this.planRoot + "/all?userId=" + userId + "&groupId=" + groupId + "&page=" + page + "&size=" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static GetMyPlansPage(userId: number, page: number, size: number) {
        return new Promise<IPlan[]>((resolve, reject) => {
            axios.get(this.planRoot + "/my?userId=" + userId  + "&page=" + page + "&size=" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static GetSimilarPlansPage(page: number, size: number, startTime: Date,
        endTime: Date, userId: number, groupId: number, activityDescription: string) {
        let input = {
            UserID: userId,
            GroupID: groupId,
            ActivityDescription: activityDescription,
            StartTime: startTime,
            EndTime: endTime
        }
        return new Promise<IPlan[]>((resolve, reject) => {

            axios.post(this.planRoot + "/similar?page=" + page + "&size=" + size, input).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static AddNewPlan(groupId: number, activity: string, startTime: Date, endTime: Date, address: string) {
        let plan = {
            Activity: activity, 
            GroupID: groupId, 
            StartTime: startTime, 
            EndTime: endTime, 
            Address: address
        }
        let userId = JSON.parse(AuthService.getUserData()).id;

        return new Promise<IPlan[]>((resolve, reject) => {
            axios.post(this.planRoot + "?userId=" + userId, plan).then((response) => {
                debugger;
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static GetUsersFromPlan(planId: number){
        return new Promise<IUser[]>((resolve, reject) => {
            axios.get(this.userRoot + "/plan/" + planId).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static DeleteUserFromPlan(planId: number, userId: number){
        return new Promise<IUser[]>((resolve, reject) => {
            axios.delete(this.planRoot + "/" + planId + "/user/" + userId).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static AddUserToPlan(planId: number, userId: number){
        let userPlan = {
            PlanID : planId,
            UserID : userId
        }
        return new Promise<IUser[]>((resolve, reject) => {
            axios.post(this.userRoot + "/plan", userPlan).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static GetPlanById(id: number){
        return new Promise<IPlan>((resolve, reject) => {
            axios.post(this.planRoot + "/" + id).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }
}