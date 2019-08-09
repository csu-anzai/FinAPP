import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpResponse,
    HttpErrorResponse,
    HttpClient
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { catchError, mergeMap, map } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    constructor(private http: HttpClient, private cookieService: CookieService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const isAvailable = this.cookieService.check('token');
        if (isAvailable) {
            const token = this.cookieService.get('token');
            // add authorization header with jwt token if available
            if (token) {
                request = request.clone({
                    setHeaders: {
                        Authorization: `Bearer ${token}`,
                        ContentType: `application/json`,
                        Application: `text/plain`
                    }
                });
                console.log(token);
            }
        }

        return next.handle(request).pipe(
            catchError((err: HttpErrorResponse) => {
                if (err.status === 401) {
                    // if (err.error.message === 'Token is exp') {

                    return this.http.post('https://localhost:44397/api/token', { accessToken: this.cookieService.get('token') })
                        .pipe(
                            mergeMap((data: any) => {
                                // If reload successful update tokens
                                // Update token
                                console.log('new token: ' + data.token);
                                this.cookieService.set('token', data.token);

                                // Clone our fieled request ant try to resend it
                                request = request.clone({
                                    setHeaders: {
                                        Authorization: `Bearer ${data.token}`,
                                        ContentType: `application/json`,
                                        Application: `text/plain`
                                    }
                                });
                                return next.handle(request).pipe(
                                    map(resp =>  resp),
                                    catchError(error => throwError(error))
                                );
                            }),
                            catchError(error => Observable.throw(error))
                        );
                }
                return Observable.throw(err);
            }
            ));
    }
}
