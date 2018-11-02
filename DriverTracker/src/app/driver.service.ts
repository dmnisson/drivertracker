import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Driver } from './driver';
import { LegService } from './leg.service';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class DriverService {
  private driversUrl = '/api/driversapi';

    getDrivers(): Observable<Driver[]> {
        return this.http.get<Driver[]>(this.driversUrl);
    }

    getDriver(id: number): Observable<Driver> {
        const url = `${this.driversUrl}/${id}`;
        return this.http.get<Driver>(url);
    }


    updateDriver(driver: Driver): Observable<any> {
        const url = `${this.driversUrl}/${driver.driverID}`;
        return this.http.put(url, driver, httpOptions);
    }

    addDriver(driver: Driver): Observable<Driver> {
        const url = `${this.driversUrl}/new`;
        return this.http.post<Driver>(url, driver, httpOptions);
    }

    deleteDriver(driver: Driver | number): Observable<Driver> {
        const id = typeof driver === 'number' ? driver : driver.driverID;
        const url = `${this.driversUrl}/${id}`;
        return this.http.delete<Driver>(url, httpOptions);
    }

    constructor(
        private http: HttpClient,
        private legService: LegService) { }
}
