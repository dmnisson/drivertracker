import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Driver } from './driver';
import { LegService } from './leg.service';

@Injectable({
  providedIn: 'root'
})
export class DriverService {
  private driversUrl = '/api/Drivers';

    getDrivers(): Observable<Driver[]> {
        return this.http.get<Driver[]>(this.driversUrl);
    }

    constructor(
        private http: HttpClient,
        private legService: LegService) { }
}
