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
    editing: number;
    deleteConfirming: number;
    detailsShowing: number;
    adding: boolean;

    getDrivers(): void {
        this.driverService.getDrivers().subscribe(drivers => this.drivers = drivers);
    }

    editClicked(driver: Driver): void {
        this.editing = driver.driverID;
    }

    cancelEdit(): void {
        this.editing = 0;
    }

    showDetails(driver: Driver): void {
        this.detailsShowing = driver.driverID;
    }

    showDeleteConfirm(driver: Driver): void {
        this.deleteConfirming = driver.driverID;
    }

    addNew(): void {
        this.adding = true;
    }

    cancelAdd(): void {
        this.adding = false;
    }

    onSubmit(): void {

    }

    constructor(private driverService: DriverService) { }

    ngOnInit() {
        this.getDrivers();
    }


}
