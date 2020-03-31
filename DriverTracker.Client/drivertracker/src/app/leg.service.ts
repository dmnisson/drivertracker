import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { HttpClient, HttpHeaders } from '../../node_modules/@angular/common/http';

import { Leg } from './leg';

import { Observable } from 'rxjs';
import { map, switchMap, catchError } from 'rxjs/operators';
import { environment } from '../environments/environment';

const jsonHeader = { 'Content-Type': 'application/json' };

@Injectable({
  providedIn: 'root'
})
export class LegService {
    private legsUrl = `${environment.apiRoot}/legsapi`;

    getLegsAll(): Observable<Leg[]> {
        return this.authService.authHeader()
            .pipe(map(ah => {return {headers: new HttpHeaders(ah)};}),
                switchMap(h => this.http.get<Leg[]>(this.legsUrl, h)));
    }

    getLegs(driverID: number): Observable<Leg[]> {
        const url = `${this.legsUrl}/fordriver/${driverID}`;
        return this.authService.authHeader()
            .pipe(map(ah => {return {headers: new HttpHeaders(ah)};}),
                switchMap(h => {return this.http.get<Leg[]>(url, h);}));
    }

    getLeg(id: number): Observable<Leg> {
        const url = `${this.legsUrl}/${id}`;
        return this.authService.authHeader()
            .pipe(map(ah => {return {headers: new HttpHeaders(ah)};}),
                switchMap(h => this.http.get<Leg>(url, h)));
    }

    createLeg(leg: Leg): Observable<any> {
        const url = `${this.legsUrl}/new`;
        return this.authService.authHeader()
            .pipe(map(ah => {return {headers: new HttpHeaders(Object.assign(ah, jsonHeader))};}),
                switchMap(h => this.http.post(url, leg, h)));
    }

    updateLeg(leg: Leg): Observable<any> {
        const url = `${this.legsUrl}/${leg.legID}`;
        return this.authService.authHeader()
            .pipe(map(ah => {return {headers: new HttpHeaders(Object.assign(ah, jsonHeader))};}),
                switchMap(h => this.http.put(url, leg, h)));
    }

    deleteLeg(leg: Leg): Observable<any> {
        const url = `${this.legsUrl}/${leg.legID}`;
        return this.authService.authHeader()
            .pipe(map(ah => {return {headers: new HttpHeaders(ah)};}),
                switchMap(h => this.http.delete(url, h)));
    }

    constructor(private http: HttpClient,
        private authService: AuthService) { }
}
