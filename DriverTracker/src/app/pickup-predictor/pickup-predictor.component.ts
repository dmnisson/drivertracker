import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PickupPredictorService } from '../pickup-predictor.service';
import { GeocodingService } from '../geocoding.service';
import { Observable, combineLatest } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-pickup-predictor',
  templateUrl: './pickup-predictor.component.html',
  styleUrls: ['./pickup-predictor.component.scss']
})
export class PickupPredictorComponent implements OnInit {
        constructor(private pickupPredictorService: PickupPredictorService,
        private geocodingService: GeocodingService,
        private aRoute: ActivatedRoute) { }

    startAddress: string;
    startCoords: number[];
    endAddress: string;
    endCoords: number[];
    delay: number;
    duration: number;
    interval: number;

    pickups: number;
    fare: number;

    fareClassProbabilities: number[];
    pickupProbabilities: number[];
    pickupProbabilityIndices: number[];

    fareClassIntervalBoundaries: number[];
    fareClassIndices: number[];

    geocodeInputAddresses(geoDependent: ((_1: number[], _2: number[]) => Observable<any>)): Observable<any> {
        const startCoords$ = this.geocodingService.getAddressCoordinates(this.startAddress);
        const endCoords$ = this.geocodingService.getAddressCoordinates(this.endAddress);

        return combineLatest(startCoords$, endCoords$, (startCoords, endCoords) => ({startCoords, endCoords}))
            .pipe(switchMap(pair => {
                this.startCoords = pair.startCoords;
                this.endCoords = pair.endCoords;

                return geoDependent(this.startCoords, this.endCoords);
            }));
    }

    onSubmit(): void {
        this.geocodeInputAddresses((s, e) => this.pickupPredictorService.getFareClassProbabilities(
            s, e, this.delay, 
            this.duration, this.pickups, this.interval))
            .subscribe(probs => this.fareClassProbabilities = probs);
    }
    
    predictPickupProbabilities(): void {
        this.geocodeInputAddresses((s, e) => this.pickupPredictorService.getPickupProbabilities(
            s, e, this.delay, 
            this.duration, this.fare, this.interval))
            .subscribe(probs => {
                this.pickupProbabilities = probs;
                this.pickupProbabilityIndices = (new Array(probs.length)).fill(0).map((x,i)=>i);
            });
    }

    ngOnInit() {
        this.pickupPredictorService.getFareClassIntervals()
            .subscribe(bounds => this.fareClassIntervalBoundaries = bounds);
        this.fareClassIndices = (new Array(this.fareClassIntervalBoundaries.length))
            .fill(0).map((x,i)=>i);
    }

}
