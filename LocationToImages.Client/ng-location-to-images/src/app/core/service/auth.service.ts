import { Injectable } from '@angular/core';
import { of, Observable, throwError, delay, tap, shareReplay } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Response } from '@data/schema/response.model';
import { JwtRequest } from '@data/schema/jwt-request.model';
import { environment } from '@env/environment';
import { Token } from '@data/schema/token.modet';

const TOKEN_NAME = 'location_to_image_token';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _token!: Token | null;

  constructor(private http: HttpClient, private router: Router) { }

  login(username: string, password: string): Observable<Response<Token>> {
    let jwtRequest: JwtRequest = { username: username, password: password };

    return this.http
      .post<Response<Token>>(
        `${environment.baseApiUrl}/users/authenticate`,
        jwtRequest
      );
  }

  public setSession(tokenResponse: Response<Token>) {
    let token: Token = tokenResponse.Data;
    this._token = token;
    localStorage.setItem(TOKEN_NAME, JSON.stringify(token));
  }

  clearStorage() {
    this._token = null;
    localStorage.removeItem(TOKEN_NAME);
  }

  logout() {
    this.clearStorage();
    this.router.navigate(['/auth/login']);
  }

  isLoggedIn(): boolean {
    return !!this.token();
  }

  isLoggedOut(): boolean {
    return !this.isLoggedIn();
  }

  token(): Token | null {
    let token: string | null = localStorage.getItem(TOKEN_NAME);

    if (!token) {
      return null;
    }

    this._token = JSON.parse(token);

    return this._token;
  }
}
