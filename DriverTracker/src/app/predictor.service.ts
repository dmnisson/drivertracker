import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class PredictorService {
    private analysisUrl = '/api/analysisapi';

    getRidershipProbabilities(driverID: number, delay: number, duration: number, fare: number): Observable<number[]> {
        const url = `${this.analysisUrl}/multipickupprob/${driverID}/${delay}/${duration}/${fare}`;
        return this.http.get<number[]>(url, httpOptions);
    }

    constructor(private http: HttpClient) { }
}
