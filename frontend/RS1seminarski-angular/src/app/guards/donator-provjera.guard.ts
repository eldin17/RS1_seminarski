import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import { Observable } from 'rxjs';
import {AuthHelper} from "../helpers/auth-helper";

@Injectable({
  providedIn: 'root'
})
export class DonatorProvjeraGuard implements CanActivate {
  constructor(private router:Router) {
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean{
    if(AuthHelper.getAuthInfo().uloga!="Donator"){
      this.router.navigate(['/app']);
      return false;
    }
    return true;
  }

}
