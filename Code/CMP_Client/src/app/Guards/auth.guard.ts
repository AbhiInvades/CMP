import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthHttpService } from 'src/app/Services/HttpServices/AuthHttpService';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService : AuthHttpService, private router:Router){}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean  {
    if(this.authService.isLoggedIn()){
      return true;
    }else {
      this.router.navigate(['login']);
      return false;
    }
  }

}
