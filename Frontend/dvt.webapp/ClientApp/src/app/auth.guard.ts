import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { NotifierService } from 'angular-notifier';
import { AuthenticationService } from './_api';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router, notifier: NotifierService,
     private tokenAuth: AuthGuard, 
     private authenticationService: AuthenticationService) {
    this.notifier = notifier
  }
  private notifier: NotifierService
  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {


    if (localStorage.getItem('currentUser')) {
      return true;
    }

    this.router.navigate(['/login']);
    this.notifier.notify('warning', 'You are not authorized to access this service, Please Login to View content');
    return false;

  }
}
