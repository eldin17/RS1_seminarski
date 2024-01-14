import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import { Observable } from 'rxjs';
import {AuthHelper} from "../helpers/auth-helper";

@Injectable({
  providedIn: 'root'
})
export class LoginProvjeraGuard implements CanActivate {
  constructor(private router:Router) {
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean{
    if(!AuthHelper.isLoggedIn()){
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}
