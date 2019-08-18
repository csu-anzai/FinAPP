import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpClient,
    HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from 'src/app/services/auth.service';
import { catchError, switchMap, filter, take } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
    private isRefreshing = false;

    constructor(private http: HttpClient,
        public authService: AuthService,
        private cookieService: CookieService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (request.url.includes('localhost')) {
            request = request.clone({
                setHeaders: {
                    ContentType: `application/json`,
                    Application: `application/json`
                }
            });

            if (this.cookieService.check('token')) {
                return next.handle(this.addToken(request, this.cookieService.get('token'))).pipe(catchError(error => {
                    if (error instanceof HttpErrorResponse && error.status === 401) {
                        return this.handle401Error(request, next);
                    } else {
                        return throwError(error);
                    }
                }));
            }
        }

        return next.handle(request);
    }

    private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
        if (!this.isRefreshing) {
            this.isRefreshing = true;
            this.refreshTokenSubject.next(null);

            return this.authService.refreshToken().pipe(
                switchMap((data: any) => {
                    this.isRefreshing = false;
                    this.refreshTokenSubject.next(data.token);
                    return next.handle(this.addToken(request, data.token));
                }));

        } else {
            return this.refreshTokenSubject.pipe(
                filter(token => token != null),
                take(1),
                switchMap(jwt => {
                    return next.handle(this.addToken(request, jwt));
                }));
        }
    }

    private addToken(request: HttpRequest<any>, token: string) {
        return request.clone({
            setHeaders: {
                'Authorization': `Bearer ${token}`
            }
        });
    }
}
