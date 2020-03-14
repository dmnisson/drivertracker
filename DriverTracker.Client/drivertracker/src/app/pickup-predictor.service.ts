import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map, flatMap } from 'rxjs/operators';
import { AuthService } from './auth.service';
import { environment } from '../environments/environment';

const jsonHeader = { 'Content-Type': 'application/json' };

@Injectable({
  providedIn: 'root'
})
export class PickupPredictorService {
    private analysisUrl = `${environment.apiRoot}/analysisapi`;

    getFareClassIntervals(): Observable<number[]> {
        const url = `${this.analysisUrl}/fareclassintervals`;
        return this.authService.authHeader().pipe(
            map(ah => {return {headers: new HttpHeaders(Object.assign(ah, jsonHeader))};}),
            flatMap(options => this.http.get<number[]>(url, options)));
    }

    setFareClassIntervals(intervalBounds: number[]): Observable<any> {
        const url = `${this.analysisUrl}/fareclassintervals`;
        return this.authService.authHeader().pipe(
            map(ah => {return {headers: new HttpHeaders(Object.assign(ah, jsonHeader))};}),
            flatMap(options => this.http.put(url, intervalBounds, options)));
    }

    getFareClassProbabilities(startCoords: number[], endCoords: number[], delay: number, duration: number, pickups: number, interval: number) : Observable<number[]> {
        const url = `${this.analysisUrl}/fareclassprob/${startCoords[0]}/${startCoords[1]}/${endCoords[0]}/${endCoords[1]}/${delay}/${duration}/${pickups}/${interval}`;
        return this.authService.authHeader().pipe(
            map(ah => {return {headers: new HttpHeaders(Object.assign(ah, jsonHeader))};}),
            flatMap(options => this.http.get<number[]>(url, options)));
    }

    getPickupProbabilities(startCoords: number[], endCoords: number[], delay: number, duration: number, fare: number, interval: number): Observable<number[]> {
        const url = `${this.analysisUrl}/pickupprob/${startCoords[0]}/${startCoords[1]}/${endCoords[0]}/${endCoords[1]}/${delay}/${duration}/${fare}/${interval}`;
        return this.authService.authHeader().pipe(
            map(ah => {return {headers: new HttpHeaders(Object.assign(ah, jsonHeader))};}),
            flatMap(options => this.http.get<number[]>(url, options)));
    }

    constructor(private http : HttpClient,
        private authService : AuthService) { }
}
