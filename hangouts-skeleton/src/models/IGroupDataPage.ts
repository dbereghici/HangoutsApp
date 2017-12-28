import { IGroup } from "./IGroup";

export interface IGroupDataPage {
    totalCount : number,
    pageSize : number,
    currentPage : number,
    previousPage : string,
    nextPage : string,
    users : IGroup[]
} 