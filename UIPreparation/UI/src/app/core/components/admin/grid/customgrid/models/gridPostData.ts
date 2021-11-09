import { FilterModel } from "./filterModel";

export class GridPostData{
    constructor(PageIndex:number,PageSize:number)
    {
        this.pageIndex=PageIndex;
        this.pageSize=PageSize
    }
    pageIndex:number
    pageSize:number
    orderByColumnName:string
    filterModelList:FilterModel[];
}