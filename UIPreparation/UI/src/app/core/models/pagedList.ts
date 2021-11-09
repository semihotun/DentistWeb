import { GridPropertyInfo } from "../components/admin/grid/customgrid/models/gridPropertyInfo";

export interface PagedList<T> {
    data:T[];
    pageIndex: number;
    pageSize: number;
    totalCount: number;
    totalPages: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
    propertyInfos:GridPropertyInfo[];
}

