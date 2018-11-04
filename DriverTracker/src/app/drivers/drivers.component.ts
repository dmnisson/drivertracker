import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

import { Driver } from '../driver';
import { DriverService } from '../driver.service';

const DELETE_CONFIRMED: string = "deleteConfirmed";
const DELETE_CANCELLED: string = "deleteCancelled";
const CROSS_CLICKED: string = "crossClicked";

@Component({
  selector: 'app-drivers',
  templateUrl: './drivers.component.html',
  styleUrls: ['./drivers.component.sass']
})
export class DriversComponent implements OnInit {

    drivers: Driver[];
    editing: number;
    adding: boolean;
    toDelete: Driver;

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

    showDeleteConfirm(content, driver: Driver) {
        this.toDelete = driver;
        this.modalService.open(content, {ariaLabelledBy: 'deleteConfirmLabel'})
            .result.then((result) => {
                if (result == DELETE_CONFIRMED) {
                    this.deleteDriver(driver);
                }
                else {
                    this.cancelDelete();
                }
            }, (reason) => {
                this.cancelDelete();
            });
    }

    deleteDriver(driver: Driver) {
        this.driverService.deleteDriver(driver).subscribe(x => {
            var indexToDelete = this.drivers.indexOf(this.toDelete, 0);
            this.drivers.splice(indexToDelete, 1);
            this.toDelete = null;
        });
    }

    cancelDelete() {
        this.toDelete = null;
    }

    constructor(private driverService: DriverService, private modalService: NgbModal) {
        this.editing = 0;
        this.adding = false;
    }

    ngOnInit() {
        this.getDrivers();
    }


}
