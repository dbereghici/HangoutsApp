import axios from 'axios';
import { IPlan } from '../models/IPlan';

export class UsersService {
    private static planRoot: string = 'http://localhost:15195/api/plan';

    public static GetAllPlansPage(userId: number, startTime: Date, endTime: Date, activityDescription: string, page: number, size: number) {
        return new Promise<IPlan[]>((resolve, reject) => {
            // [HttpGet("all/")]
            // (int userId, DateTime startTime,
            //     DateTime endTime, string activityDescription, int page, int size)
            axios.get(this.planRoot + "/all/" ).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

    public static GetSimilarPlansPage(id: number, page: number, size: number) {
        // [HttpGet("similar/")]
        // (int userId, int groupId, DateTime startTime, 
        //     DateTime endTime, string activityDescription, int page, int size)
        return new Promise<IPlan[]>((resolve, reject) => {
            axios.get(this.planRoot + "/" + id + "/page/" + page + "/size/" + size).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }


}