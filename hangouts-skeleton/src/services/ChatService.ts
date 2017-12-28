import axios from 'axios';
import { IChat } from '../models/IChat';

export default class FriendService {
    private static chatRoot: string = 'http://localhost:15195/api/chat';

    public static getChatOfFriendship(id1 : number, id2 : number){
        return new Promise<IChat>((resolve, reject) => {
            axios.get(this.chatRoot + "/friendship?id1=" + id1 + "&id2=" + id2).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

}