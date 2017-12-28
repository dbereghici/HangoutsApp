import { IUser } from "./IUser";
import { IMessage } from "./IMessage";

export interface IChat {
    id : number,
    messages : Array<IMessage>,
    users : Array<IUser>;
} 