import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from 'src/app/services/auth.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    constructor(public authService: AuthService, private cookieService: CookieService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (request.url.includes('localhost')) {
            request = request.clone({
                setHeaders: {
                    ContentType: `application/json`,
                    Application: `application/json`
                }
            });
            if (this.cookieService.check('token')) {
                return next.handle(this.addToken(request, this.cookieService.get('token')));
            }
        }
        return next.handle(request);
    }

    private addToken(request: HttpRequest<any>, token: string) {
        return request.clone({
            setHeaders: {
                'Authorization': `Bearer ${token}`
            }
        });
    }
}
