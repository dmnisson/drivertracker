import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map, flatMap } from 'rxjs/operators';

import { CompanyStatistics, DriverStatistics } from './statistics';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {

    private analysisUrl = '/api/analysisapi';

    getCompanyStatistics(): Observable<CompanyStatistics> {
        const url = `${this.analysisUrl}/company`;
        return this.authService.authHeader()
            .pipe(map(ah => {return {headers: new HttpHeaders(ah)};}),
                flatMap(h => this.httpClient.get<CompanyStatistics>(url, h)));
    }

    getDriverStatistics(id: number): Observable<DriverStatistics> {
        const url = `${this.analysisUrl}/${id}`;
        return this.authService.authHeader()
            .pipe(map(ah => {return {headers: new HttpHeaders(ah)};}),
                flatMap(h => this.httpClient.get<DriverStatistics>(url, h)));
    }

    constructor(private httpClient: HttpClient,
        private authService: AuthService) {

    }
}
