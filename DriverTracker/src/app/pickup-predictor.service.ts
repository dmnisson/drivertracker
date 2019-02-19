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
export class PickupPredictorService {
    private analysisUrl = '/api/analysisapi';

    getFareClassIntervals(): Observable<number[]> {
        const url = `${this.analysisUrl}/fareclassintervals`;
        return this.http.get<number[]>(url, httpOptions);
    }

    setFareClassIntervals(intervalBounds: number[]): Observable<any> {
        const url = `${this.analysisUrl}/fareclassintervals`;
        return this.http.put(url, intervalBounds, httpOptions);
    }

    getFareClassProbabilities(startCoords: number[], endCoords: number[], delay: number, duration: number, pickups: number, interval: number) : Observable<number[]> {
        const url = `${this.analysisUrl}/fareclassprob/${startCoords[0]}/${startCoords[1]}/${endCoords[0]}/${endCoords[1]}/${delay}/${duration}/${pickups}/${interval}`;
        return this.http.get<number[]>(url, httpOptions);
    }

    getPickupProbabilities(startCoords: number[], endCoords: number[], delay: number, duration: number, fare: number, interval: number): Observable<number[]> {
        const url = `${this.analysisUrl}/pickupprob/${startCoords[0]}/${startCoords[1]}/${endCoords[0]}/${endCoords[1]}/${delay}/${duration}/${fare}/${interval}`;
        return this.http.get<number[]>(url, httpOptions);
    }

    constructor(private http : HttpClient) { }
}
