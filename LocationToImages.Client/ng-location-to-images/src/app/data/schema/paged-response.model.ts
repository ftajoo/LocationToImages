import { Response } from './response.model';

export interface PagedResponse<T> extends Response<T> {
  PageNumber: number;
  PageSize: number;
  TotalPages: number;
  TotalRecords: number;
}
