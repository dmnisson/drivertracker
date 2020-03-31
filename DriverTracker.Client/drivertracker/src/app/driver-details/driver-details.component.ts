import { Component, OnInit } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

import { DriverStatistics } from '../statistics';
import { Driver } from '../driver';
import { Leg } from '../leg';

import { DriverService } from '../driver.service';
import { LegService } from '../leg.service';
import { StatisticsService } from '../statistics.service';
import { AuthService } from '../auth.service';
import { ActivatedRoute } from '../../../node_modules/@angular/router';

import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { faEdit } from '@fortawesome/free-solid-svg-icons';


const LEG_DETAILS_CROSS_CLICK = 'legDetailsCrossClick';
const LEG_DETAILS_SAVE_CLICK = 'legDetailsSaveClick';
const LEG_DETAILS_CLOSE_CLICK = 'legDetailsCloseClick';
const LEG_DELETE_CONFIRM_CROSS_CLICK = 'legDeleteConfirmCrossClick';
const LEG_DELETE_CONFIRM_CLICK = 'legDeleteConfirmClick';
const LEG_DELETE_CANCEL_CLICK = 'legDeleteCancelClick';

@Component({
  selector: 'app-driver-details',
  templateUrl: './driver-details.component.html',
  styleUrls: ['./driver-details.component.sass']
})
export class DriverDetailsComponent implements OnInit {
    faEdit = faEdit;

    driver: Driver;
    driverStatistics: DriverStatistics;
    legs: Leg[];
    
    editingDriverDetails = false;

    editingLegDetails = false;
    showingLeg: Leg;
    addingLeg = false;
    legToDelete: Leg;

    constructor(private driverService: DriverService,
        private legService: LegService,
        private statisticsService: StatisticsService,
        private authService: AuthService,
        private aRoute: ActivatedRoute,
        private ngbModal: NgbModal) {
        this.aRoute.params.subscribe(p => this.loadDriver(p['id']));
    }

    ngOnInit() {
    }

    private loadDriver(id: number) {
        this.driverService.getDriver(id).
        subscribe(d => {
            this.driver = d;
            this.getDriverStatistics();
            this.loadLegs();
        });
    }

    private getDriverStatistics() {
        this.statisticsService.getDriverStatistics(this.driver.driverID)
            .subscribe(s => {this.driverStatistics = s;});
    }

    private loadLegs() {
        this.legService.getLegs(this.driver.driverID)
            .subscribe(a => {this.legs = a;});
    }

    clickEditDriverDetails() {
        this.editingDriverDetails = true;
    }

    cancelEditDriverDetails() {
        this.driverService.getDriver(this.driver.driverID)
        .subscribe(drv => {this.driver = drv; this.editingDriverDetails = false;});
    }

    onSubmitDriverDetails() {
        if (this.editingDriverDetails) {
            this.driverService.updateDriver(this.driver)
            .subscribe(x => {this.editingDriverDetails = false;});
        }
    }

    showLegDetails(leg: Leg, legDetails) {
        if (leg == null) {
            this.showingLeg = new Leg();
            this.showingLeg.driverID = this.driver.driverID;
            this.addingLeg = true;
            this.editingLegDetails = true;
        } else {
            this.showingLeg = leg;
            this.addingLeg = false;
        }

        this.ngbModal.open(legDetails, {ariaLabelledBy: 'legDetailsLabel'}).result
            .then((result) => {
                if (result == LEG_DETAILS_SAVE_CLICK) {
                    console.log(result);
                    this.onSubmitLegDetails();
                } else {
                    this.cancelEditLegDetails();
                }
            }, (reason) => {
                this.cancelEditLegDetails();
            });
    }

    editShowingLeg() {
        this.editingLegDetails = true;
    }

    onSubmitLegDetails() {
        if (this.editingLegDetails) {
            var cuObs: Observable<any>;
            if (this.addingLeg) {
                cuObs = this.legService.createLeg(this.showingLeg);
            }
            else {
                cuObs = this.legService.updateLeg(this.showingLeg);

            }
            cuObs.subscribe(x => {
                this.editingLegDetails = false;
                this.getDriverStatistics();
                if (this.addingLeg) {
                    this.legs.push(this.showingLeg);
                    this.addingLeg = false;
                }
                this.showingLeg = null;
            });
        }
    }

    cancelEditLegDetails() {
        if (this.editingLegDetails) {
            this.legService.getLeg(this.showingLeg.legID)
                .subscribe(leg => {
                    var indexToRestore = this.legs.indexOf(this.showingLeg);
                    this.legs[indexToRestore] = leg;
                    this.showingLeg = null;
                    this.editingLegDetails = false;
                });
        }
    }

    showDeleteConfirm(leg: Leg, deleteConfirm) {
        this.legToDelete = leg;
        this.ngbModal.open(deleteConfirm, {ariaLabelledBy: 'legDeleteConfirmLabel'}).result
            .then((result) => {
                if (result == LEG_DELETE_CONFIRM_CLICK) {
                    this.confirmDeleteLeg();
                }
                else {
                    this.cancelDeleteLeg();
                }
            }, (reason) => {this.cancelDeleteLeg();});
    }

    confirmDeleteLeg() {
        this.legService.deleteLeg(this.legToDelete).subscribe(x => {
            var indexToDelete = this.legs.indexOf(this.legToDelete, 0);
            this.legs.splice(indexToDelete, 1);
            this.legToDelete = null;
            this.getDriverStatistics();
        });
    }

    cancelDeleteLeg() {
        this.legToDelete = null;
    }
}
