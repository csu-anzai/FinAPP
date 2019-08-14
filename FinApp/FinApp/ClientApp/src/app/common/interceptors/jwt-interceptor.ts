import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpErrorResponse,
    HttpClient
} from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { catchError, mergeMap, map, filter, take, switchMap } from 'rxjs/operators';
import { AuthService } from 'src/app/services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    private isRefreshing = false;
    private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
    jwtHelper = new JwtHelperService();

    constructor(public authService: AuthService, private cookieService: CookieService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (request.url.includes('localhost')) {
            if (this.cookieService.check('token')) {
                if (this.jwtHelper.isTokenExpired(this.cookieService.get('token'))) {
                    console.log('needs to be refreshed');
                    this.refreshing(request, next);
                }
                request = this.addToken(request, this.cookieService.get('token'));
            }
            return next.handle(request).pipe(catchError(error => {
                return throwError(error);
            }));
        } else {
            return next.handle(request);
        }
    }

    private addToken(request: HttpRequest<any>, token: string) {
        return request.clone({
            setHeaders: {
                'Authorization': `Bearer ${token}`
            }
        });
    }

    private refreshing(request: HttpRequest<any>, next: HttpHandler) {
        console.log('refreshing method');

        if (!this.isRefreshing) {
            console.log('!refreshing');
            this.isRefreshing = true;
            this.refreshTokenSubject.next(null);

            return this.authService.refreshToken()
                // .pipe(
                //     switchMap(
                //         (data: any) => {
                //             this.isRefreshing = false;
                //             this.refreshTokenSubject.next(data.token);
                //             return next.handle(this.addToken(request, data.token));
                //         }
                //     )
                // );
                .pipe(
                    catchError(err => throwError(err))
                )
                .subscribe(
                    (data: any) => {
                        // Update token
                        console.log('new token: ' + data.token);
                        this.cookieService.set('token', data.token);
                        this.isRefreshing = false;
                        this.refreshTokenSubject.next(this.cookieService.get('token'));
                        return next.handle(this.addToken(request, this.cookieService.get('token')));
                    }
                );

            // .pipe(
            //     mergeMap((response: any) => {
            //         console.log(response);
            //         return response.token;
            //     })
            // )
            // .subscribe(
            //     (data: any) => {
            //         // Update token
            //         console.log('new token: ' + data.token);
            //         this.cookieService.set('token', data.token);
            //         this.isRefreshing = false;
            //         this.refreshTokenSubject.next(data.token);
            //         return next.handle(this.addToken(request, data.token));
            //     },
            //     catchError(err => throwError(err))
            // );

        } else {
            console.log('refreshing');
            return this.refreshTokenSubject.pipe(
                filter(token => token != null),
                take(1),
                switchMap(jwt => {
                    return next.handle(this.addToken(request, jwt));
                }));
        }
    }
}
