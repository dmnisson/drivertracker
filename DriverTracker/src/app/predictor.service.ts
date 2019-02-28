import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map, flatMap } from 'rxjs/operators';
import { AuthService } from './auth.service';

const jsonHeader = { 'Content-Type': 'application/json' };

@Injectable({
  providedIn: 'root'
})
export class PredictorService {
    private analysisUrl = '/api/analysisapi';

    getRidershipProbabilities(driverID: number, delay: number, duration: number, fare: number): Observable<number[]> {
        const url = `${this.analysisUrl}/multipickupprob/${driverID}/${delay}/${duration}/${fare}`;
        return this.authService.authHeader().pipe(
                map(ah => {return {headers: new HttpHeaders(Object.assign(ah, jsonHeader))};}),
                flatMap(options => this.http.get<number[]>(url, options)));
    }

    constructor(private http: HttpClient,
        private authService: AuthService) { }
}
