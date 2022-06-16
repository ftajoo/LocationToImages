import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, of, retry, throwError } from 'rxjs';
import { AuthService } from '@core/service/auth.service';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let handled: boolean = false;

    return next.handle(request)
      .pipe(
        catchError((returnedError) => {
          let errorMessage!: string;

          if (returnedError.error instanceof ErrorEvent) {
            errorMessage = `Error: ${returnedError.error.message}`;
          } else if (returnedError instanceof HttpErrorResponse) {
            if (returnedError.status === 400) {
              errorMessage = `${returnedError.error?.Error}`;
            } else {
              errorMessage = `${returnedError.error?.Error}`;
            }

            handled = this.handleServerSideError(returnedError);
          }

          if (errorMessage) {
            return throwError(() => new Error(errorMessage));
          }

          if (handled) {
            return of(returnedError);
          }

          return throwError(() => new Error("Unexpected problem occurred"));
        })
      );
  }

  private handleServerSideError(error: HttpErrorResponse): boolean {
    let handled: boolean = false;

    switch (error.status) {
      case 401:
        this.authService.logout();
        handled = true;
        break;
      case 403:
        this.authService.logout();
        handled = true;
        break;
    }

    return handled;
  }
}
