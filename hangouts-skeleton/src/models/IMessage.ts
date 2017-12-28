import { IUser } from "./IUser";

export interface IMessage {
    id : number,
    chatid : number,
    content : string,
    createdAt : Date,
    user : IUser
} 