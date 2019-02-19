import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LegCoordinates } from './leg-coordinates';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class GeocodingService {
    private geocodingUrl = "/api/geocoding";

    getLegCoordinates(id: number): Observable<LegCoordinates> {
        const url = `${this.geocodingUrl}/${id}`;
        return this.http.get<LegCoordinates>(url, httpOptions);
    }

    getAddressCoordinates(address: string): Observable<number[]> {
        const url = `${this.geocodingUrl}/direct/${address}`;
        return this.http.get<number[]>(url, httpOptions);
    }

    constructor(private http: HttpClient) { }
}
