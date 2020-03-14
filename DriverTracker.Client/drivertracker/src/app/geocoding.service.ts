import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map, flatMap } from 'rxjs/operators';
import { LegCoordinates } from './leg-coordinates';

import { AuthService } from './auth.service';
import { environment } from '../environments/environment';

const jsonHeader = { 'Content-Type': 'application/json' };

@Injectable({
  providedIn: 'root'
})
export class GeocodingService {
    private geocodingUrl = `${environment.apiRoot}/geocoding`;

    getLegCoordinates(id: number): Observable<LegCoordinates> {
        const url = `${this.geocodingUrl}/${id}`;
        return this.authService.authHeader().pipe(
            map(ah => {return {headers: new HttpHeaders(Object.assign(ah, jsonHeader))};}),
            flatMap(options => this.http.get<LegCoordinates>(url, options)));
    }

    getAddressCoordinates(address: string): Observable<number[]> {
        const url = `${this.geocodingUrl}/direct/${address}`;
        return this.authService.authHeader().pipe(
            map(ah => {return {headers: new HttpHeaders(Object.assign(ah, jsonHeader))};}),
            flatMap(options => this.http.get<number[]>(url, options)));
    }

    constructor(private http: HttpClient,
        private authService: AuthService) { }
}
