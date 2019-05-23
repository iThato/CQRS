import { Injectable } from '@angular/core';
const TOKEN = 'TOKEN';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

  setToken(token: string): void {
    localStorage.setItem('currentUser', token);
  }

  isLogged() {
    if(localStorage.getItem('currentUser') != null){
      return true;
    }
    return false;
  }

  getAuthorizationToken(): string{
    var token = localStorage.getItem('currentUser');
    return token;
  }

  logout(){
    localStorage.removeItem('currentUser');
  }
}
