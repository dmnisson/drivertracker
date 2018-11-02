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
    adding: boolean;

    getDrivers(): void {
        this.driverService.getDrivers().subscribe(drivers => this.drivers = drivers);
    }

    editClicked(driver: Driver): void {
        this.cancelAdd();
        this.editing = driver.driverID;
    }
    
    cancelEdit(): void {
        this.driverService.getDriver(this.editing).subscribe(driver => {
            let dmem: Driver = this.drivers.find(d => d.driverID == this.editing);
            if (dmem !== null && driver !== null) {
                dmem.licenseNumber = driver.licenseNumber;
                dmem.name = driver.name;
                this.editing = 0;
            }
        });
    }

    showDeleteConfirm(driver: Driver): void {
        this.deleteConfirming = driver.driverID;
    }

    addNew(): void {
        this.cancelEdit();
        this.adding = true;
    }

    cancelAdd(): void {
        this.adding = false;
    }

    onSubmit(): void {
        if (this.editing !== 0) {
            let driver: Driver = this.drivers.find(d => d.driverID == this.editing);
            this.driverService.updateDriver(driver).subscribe(x => this.editing = 0);
        }
    }

    saveNew(name: string, licenseNumber: string): void {
        name = name.trim();
        if (!name) {return;}
        licenseNumber = licenseNumber.trim();
        if (!licenseNumber) {return;}

        this.driverService.addDriver({name, licenseNumber} as Driver)
            .subscribe(driver => this.drivers.push(driver));
    }

    constructor(private driverService: DriverService) {
        this.editing = 0;
        this.adding = false;
    }

    ngOnInit() {
        this.getDrivers();
    }


}
