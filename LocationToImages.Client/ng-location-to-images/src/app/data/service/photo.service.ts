import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PagedResponse } from '@data/schema/paged-response.model';
import { PaginationFilter } from '@data/schema/pagination-filter.model';
import { PhotoSearchBy } from '@data/schema/photo-search-by.enum';
import { Photo } from '@data/schema/photo.model';
import { Response } from '@data/schema/response.model';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {
  constructor(private http: HttpClient) { }

  searchByAddress(address: string): Observable<Response<Photo[]>> {
    const params = new HttpParams({
      fromObject: {
        address: address
      }
    });

    return this.http.get<PagedResponse<Photo[]>>(
      `${environment.baseApiUrl}/photos/search/address`,
      {params: params});
  }

  public get(search: string, photoSearchBy: PhotoSearchBy, paginationFilter: PaginationFilter): Observable<PagedResponse<Photo[]>> {
    const param = this.getSearchParam(search, photoSearchBy);

    const params = new HttpParams({
      fromObject: {
        ...param,
        'filter.pageNumber': paginationFilter.PageNumber,
        'filter.pageSize': paginationFilter.PageSize,
      }
    });

    return this.http.get<PagedResponse<Photo[]>>(this.getUrl(photoSearchBy), { params: params });
  }

  public save(photos: Photo[]): Observable<Response<Photo[]>> {
    return this.http.post<Response<Photo[]>>(`${environment.baseApiUrl}/photos/from-list`, { Photos: photos });
  }

  private getUrl(photoSearchBy: PhotoSearchBy): string {
    switch (photoSearchBy) {
      case PhotoSearchBy.Title:
        return `${environment.baseApiUrl}/photos/title`;
      case PhotoSearchBy.Description:
        return `${environment.baseApiUrl}/photos/description`;
      case PhotoSearchBy.Address:
        return `${environment.baseApiUrl}/photos/address`;
      default:
        return `${environment.baseApiUrl}/photos`;
    }
  }

  private getSearchParam(search: string, photoSearchBy: PhotoSearchBy): object {
    switch (photoSearchBy) {
      case PhotoSearchBy.Title:
        return {
          'title': search
        };
      case PhotoSearchBy.Description:
        return {
          'description': search
        };
      case PhotoSearchBy.Address:
        return {
          'address': search
        };
      default:
        return {};
    }
  }
}
