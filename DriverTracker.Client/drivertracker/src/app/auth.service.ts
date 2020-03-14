import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, BehaviorSubject, timer } from 'rxjs';
import { filter, map, switchMap, skipWhile, first } from 'rxjs/operators';
import { LoginModel } from './login-model';
import * as jwt_decode from 'jwt-decode';
import { environment } from '../environments/environment';

const jsonHeader = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
};

const AUTH_TOKEN_ITEM_KEY = 'authToken';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
    private authUrl = `${environment.apiRoot}/account`;
    private token = new BehaviorSubject<string>(
      localStorage.getItem(AUTH_TOKEN_ITEM_KEY)
    );

    constructor(private http: HttpClient) {

    }

    makeToken(email: string, password: string) : Promise<String> {
        const url = this.authUrl + '/maketoken';
      return this.http.post<string>(url, new LoginModel(email, password),
        Object.assign(jsonHeader, { responseType: 'text' as 'json' }))
        .pipe(first(t => {
          this.updateToken(t);
          return true;
        }))
        .toPromise();
    }

    refreshToken(): void {
        const url = this.authUrl + '/refreshtoken';
        this.authHeader().pipe(
            map(ah => new HttpHeaders(ah)),
            switchMap(ah => this.http.post<string>(url, "", {headers: ah})))
            .subscribe(t => this.updateToken(t));
    }

    getCurrentToken(): Observable<string> {
      return this.token.pipe(map(t => {
        const expDate = jwt_decode(t).exp;
        const currentTime = Date.now();
        return expDate < currentTime / 1000 ? null : t;
      }));
    }

    /* update the locally stored information */
    updateToken(token: string): void {
        localStorage.setItem(AUTH_TOKEN_ITEM_KEY, token);
        this.token.next(token);
        const expDate = jwt_decode(token).exp;
        timer(expDate - 15*60000).subscribe(obj => this.refreshToken());
    }

    authHeader(): Observable<any> {
        return this.getCurrentToken().pipe(map(t => {return {'Authorization': 'Bearer ' + t};}));
    }
}