import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, BehaviorSubject, timer } from 'rxjs';
import { filter, map, flatMap, skipWhile } from 'rxjs/operators';
import { LoginModel } from './login-model';
import * as jwt_decode from 'jwt-decode';

const jsonHeader = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {
    private authUrl = '/api/account';
    private token = new BehaviorSubject<string>(null);
    private tokenExpired = new BehaviorSubject<boolean>(false);

    constructor(private http: HttpClient) {

    }

    makeToken(email: string, password: string) : void {
        const url = this.authUrl + '/maketoken';
        this.http.post<string>(url, new LoginModel(email, password),
            Object.assign(jsonHeader, {responseType: 'text' as 'json'}))
            .subscribe(t => this.updateToken(t));
    }

    makeSessionUserToken(force = false) : void {
        const url = this.authUrl + '/makesessionusertoken';
        this.token.asObservable().pipe(
            filter(token => force || token == null),
            flatMap(token => this.http.post<string>(url, "", {responseType: 'text' as 'json'})))
            .subscribe(t => this.updateToken(t));
    }

    refreshToken(): void {
        const url = this.authUrl + '/refreshtoken';
        this.authHeader().pipe(
            map(ah => new HttpHeaders(ah)),
            flatMap(ah => this.http.post<string>(url, "", {headers: ah})))
            .subscribe(t => this.updateToken(t));
    }

    getCurrentToken(): Observable<string> {
        return this.token.asObservable().pipe(skipWhile(t => t == null));
    }

    isTokenExpired(): Observable<boolean> {
        return this.tokenExpired.asObservable();
    }

    // update the information stored in the class
    updateToken(token: string): void {
        this.token.next(token);
        this.tokenExpired.next(false);
        let expDate = jwt_decode(token).exp;
        timer(expDate - 15*60000).subscribe(obj => this.refreshToken());
    }

    authHeader(): Observable<any> {
        return this.getCurrentToken().pipe(map(t => {return {'Authorization': 'Bearer ' + t};}));
    }
}