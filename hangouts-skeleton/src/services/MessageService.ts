import axios from 'axios';

export default class MessageService {
    private static messageRoot: string = 'http://localhost:15195/api/message';

    public static addMessage(message: any){
        return new Promise((resolve, reject) => {
            axios.post(this.messageRoot, message).then((response) => {
                resolve(response.data);
            },
                (error: any) => {
                    reject(error);
                });
        });
    }

}