import { IUser } from "./IUser";

export interface IUserDataPage {
    totalCount : number,
    pageSize : number,
    currentPage : number,
    previousPage : string,
    nextPage : string,
    users : IUser    
} 