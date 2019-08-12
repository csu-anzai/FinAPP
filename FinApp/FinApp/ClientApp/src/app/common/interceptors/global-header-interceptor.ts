import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpClient
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

@Injectable()
export class GlobalHeaderInterceptor implements HttpInterceptor {

    constructor(private http: HttpClient, private cookieService: CookieService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const isAvailable = this.cookieService.check('token');
        if (isAvailable) {
            const token = this.cookieService.get('token');
            // add authorization header with jwt token if available
            if (token) {
                request = request.clone({
                    setHeaders: {
                        ContentType: `application/json`,
                        Application: `text/plain`
                    }
                });
            }
        }

        return next.handle(request);
    }
}