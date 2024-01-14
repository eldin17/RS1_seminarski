import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {AuthHelper} from "../helpers/auth-helper";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const obj:any = AuthHelper.getAuthInfo().token;


    if (obj) {
      req = req.clone({
        setHeaders: { Authorization: `Bearer ${obj}` },
      });
    }

    return next.handle(req);
  }
}
