import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

@Injectable()
export class AuthService {
  constructor(private http: HttpClient, private router: Router) { }
  baseUrl: string = '/Users';
    login(req: any) {
    console.log(req);
    return this.http.post(this.baseUrl + "/Login", req)
      .pipe(map(user => {
        // login successful if there's a jwt token in the response
        if (user && user["token"]) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(user));
          localStorage.setItem('newUser', "false");
        }
        return user;
      }));
  }

    register(req: any) {
        console.log(req);
        return this.http.post(this.baseUrl + "/Register", req)
          .pipe(map(user => {
            // login successful if there's a jwt token in the response
            if (user && user["token"]) {
              // store user details and jwt token in local storage to keep user logged in between page refreshes
              localStorage.setItem('currentUser', JSON.stringify(user));
              localStorage.setItem('newUser', "true");
            }
            return user;
          }));
  }
}

