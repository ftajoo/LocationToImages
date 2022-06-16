import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '@core/service/auth.service';
import { GeoLocation } from '@data/schema/geo-location.model';
import { Response } from '@data/schema/response.model';
import { UserInsert } from '@data/schema/user-insert.model';
import { User } from '@data/schema/user.model';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private _authService: AuthService) { }

  register(userInsert: UserInsert): Observable<Response<User>> {
    return this.http
      .post<Response<User>>(
        `${environment.baseApiUrl}/users`,
        userInsert
      );
  }

  getGeoLocations(): Observable<Response<GeoLocation[]>> {
    return this.http
      .get<Response<GeoLocation[]>>(
        `${environment.baseApiUrl}/users/${this._authService.token()?.User.Id}/geo-locations`
      );
  }

  addGeoLocation(geoLocation: GeoLocation): Observable<Response<GeoLocation>> {
    return this.http
      .post<Response<GeoLocation>>(
        `${environment.baseApiUrl}/users/${this._authService.token()?.User.Id}/geo-locations`,
        geoLocation
      );
  }

  deleteGeoLocation(geoLocation: GeoLocation): Observable<Response<number>> {
    return this.http
      .delete<Response<number>>(
        `${environment.baseApiUrl}/users/${this._authService.token()?.User.Id}/geo-locations/${geoLocation.Id}`
      );
  }
}
