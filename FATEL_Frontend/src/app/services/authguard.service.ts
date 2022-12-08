import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from "@angular/router";
import {Observable} from "rxjs";
import jwtDecode from "jwt-decode";
import {Token} from "../entities/token";

@Injectable({
  providedIn: 'root'
})
export class AuthguardService implements CanActivate{

  constructor(private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let token = localStorage.getItem('token');
    if(token){
      let decoded = jwtDecode(token) as Token;
      let currentDate = new Date();
      // @ts-ignore
      let expiry = new Date(decoded.exp*1000);
      if(currentDate < expiry) {
        return true;
      }
    }
    if (window.innerWidth <= 500) {
      this.router.navigate(['./mobileLogin'])
      return false
    }
    else {
      this.router.navigate(['./login'])
      return false;
      }
    }
}
