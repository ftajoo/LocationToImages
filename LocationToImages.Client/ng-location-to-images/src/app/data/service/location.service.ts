import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GeoLocation } from '@data/schema/geo-location.model';
import { Location } from '@data/schema/location.model';
import { Response } from '@data/schema/response.model';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  constructor(private http: HttpClient) { }

  public getLocations(): Observable<Response<Location[]>> {
    return this.http.get<Response<Location[]>>(`${environment.baseApiUrl}/geo-locations`);
  }

  public getLocation(address: string): Observable<Response<GeoLocation>> {
    return this.http.get<Response<GeoLocation>>(`${environment.baseApiUrl}/geo-locations/${address}`);
  }
}
