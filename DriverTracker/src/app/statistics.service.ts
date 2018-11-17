import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { CompanyStatistics } from './statistics';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {

    private analysisUrl = '/api/analysisapi';

    getCompanyStatistics(): Observable<CompanyStatistics> {
        const url = `${this.analysisUrl}/company`
        return this.httpClient.get<CompanyStatistics>(url);
    }

    constructor(private httpClient: HttpClient) { }
}
