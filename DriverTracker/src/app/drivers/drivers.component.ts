import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { Driver } from '../driver';
import { DriverService } from '../driver.service';

@Component({
  selector: 'app-drivers',
  templateUrl: './drivers.component.html',
  styleUrls: ['./drivers.component.sass']
})
export class DriversComponent implements OnInit {

  drivers: Driver[];

    getDrivers(): void {
        this.driverService.getDrivers().subscribe(drivers => this.drivers = drivers);
    }

    constructor(private driverService: DriverService) { }

    ngOnInit() {
        this.getDrivers();
    }

}
