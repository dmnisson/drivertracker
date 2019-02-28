import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, flatMap, catchError } from 'rxjs/operators';

import { Driver } from './driver';
import { LegService } from './leg.service';
import { AuthService } from './auth.service';

const jsonHeader = { 'Content-Type': 'application/json' };

@Injectable({
  providedIn: 'root'
})
export class DriverService {
  private driversUrl = '/api/driversapi';

    getDrivers(): Observable<Driver[]> {
        return this.authService.authHeader().pipe(
            map(ah => {return {headers: new HttpHeaders(ah)};}),
            flatMap(h => this.http.get<Driver[]>(this.driversUrl, h)));
    }

    getDriver(id: number): Observable<Driver> {
        const url = `${this.driversUrl}/${id}`;
        return this.authService.authHeader().pipe(
            map(ah => {return {headers: new HttpHeaders(ah)};}),
            flatMap(h => this.http.get<Driver>(url, h)));
    }


    updateDriver(driver: Driver): Observable<any> {
        const url = `${this.driversUrl}/${driver.driverID}`;
        return this.authService.authHeader().pipe(
            map(ah => {return {headers: new HttpHeaders(Object.assign(ah, jsonHeader))};}),
            flatMap(options => this.http.put(url, driver, options)));
    }

    addDriver(driver: Driver): Observable<Driver> {
        const url = `${this.driversUrl}/new`;
        return this.authService.authHeader().pipe(
            map(ah => {return {headers: new HttpHeaders(Object.assign(ah, jsonHeader))};}),
            flatMap(options => this.http.post<Driver>(url, driver, options)));
    }

    deleteDriver(driver: Driver | number): Observable<Driver> {
        const id = typeof driver === 'number' ? driver : driver.driverID;
        const url = `${this.driversUrl}/${id}`;
        return this.authService.authHeader().pipe(
            map(ah => {return {headers: new HttpHeaders(Object.assign(ah, jsonHeader))};}),
            flatMap(options => this.http.delete<Driver>(url, options)));
    }
    

    constructor(
        private http: HttpClient,
        private legService: LegService,
        private authService: AuthService) {

    }
}
