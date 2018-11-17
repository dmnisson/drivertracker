import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';


import { Driver } from '../driver';
import { PredictorService } from '../predictor.service';
import { DriverService } from '../driver.service';

@Component({
  selector: 'app-predictor',
  templateUrl: './predictor.component.html',
  styleUrls: ['./predictor.component.sass']
})
export class PredictorComponent implements OnInit {

    driver: Driver;
    delay: number;
    duration: number;
    fare: number;
    ridershipProbabilities: number[];

    getDriver(id: number): void {
        this.driverService.getDriver(id).subscribe(driver => this.driver = driver);
    }

    onSubmit(): void {

        this.predictorService.getRidershipProbabilities(this.driver.driverID, this.delay, this.duration, this.fare)
        .subscribe(probs => this.ridershipProbabilities = probs);
    }

    constructor(private predictorService: PredictorService, 
        private driverService: DriverService,
        private aRoute: ActivatedRoute) { 
        this.aRoute.params.subscribe(p => this.getDriver(p['id']));
    }

    ngOnInit() {

    }

}
