(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["main"],{

/***/ "./src/$$_lazy_route_resource lazy recursive":
/*!**********************************************************!*\
  !*** ./src/$$_lazy_route_resource lazy namespace object ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

function webpackEmptyAsyncContext(req) {
	// Here Promise.resolve().then() is used instead of new Promise() to prevent
	// uncaught exception popping up in devtools
	return Promise.resolve().then(function() {
		var e = new Error("Cannot find module '" + req + "'");
		e.code = 'MODULE_NOT_FOUND';
		throw e;
	});
}
webpackEmptyAsyncContext.keys = function() { return []; };
webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
module.exports = webpackEmptyAsyncContext;
webpackEmptyAsyncContext.id = "./src/$$_lazy_route_resource lazy recursive";

/***/ }),

/***/ "./src/app/app-routing.module.ts":
/*!***************************************!*\
  !*** ./src/app/app-routing.module.ts ***!
  \***************************************/
/*! exports provided: AppRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppRoutingModule", function() { return AppRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm2015/router.js");
/* harmony import */ var _drivers_drivers_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./drivers/drivers.component */ "./src/app/drivers/drivers.component.ts");
/* harmony import */ var _predictor_predictor_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./predictor/predictor.component */ "./src/app/predictor/predictor.component.ts");
/* harmony import */ var _pickup_predictor_pickup_predictor_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./pickup-predictor/pickup-predictor.component */ "./src/app/pickup-predictor/pickup-predictor.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};





const routes = [
    { path: 'Drivers', component: _drivers_drivers_component__WEBPACK_IMPORTED_MODULE_2__["DriversComponent"] },
    { path: 'Predictor/Index/:id', component: _predictor_predictor_component__WEBPACK_IMPORTED_MODULE_3__["PredictorComponent"] },
    { path: 'PickupPredictor', component: _pickup_predictor_pickup_predictor_component__WEBPACK_IMPORTED_MODULE_4__["PickupPredictorComponent"] }
];
let AppRoutingModule = class AppRoutingModule {
};
AppRoutingModule = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
        imports: [
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forRoot(routes)
        ],
        exports: [
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]
        ]
    })
], AppRoutingModule);



/***/ }),

/***/ "./src/app/app.component.html":
/*!************************************!*\
  !*** ./src/app/app.component.html ***!
  \************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<router-outlet></router-outlet>"

/***/ }),

/***/ "./src/app/app.component.sass":
/*!************************************!*\
  !*** ./src/app/app.component.sass ***!
  \************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJhcHAuY29tcG9uZW50LnNhc3MifQ== */"

/***/ }),

/***/ "./src/app/app.component.ts":
/*!**********************************!*\
  !*** ./src/app/app.component.ts ***!
  \**********************************/
/*! exports provided: AppComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppComponent", function() { return AppComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

let AppComponent = class AppComponent {
    constructor() {
        this.title = 'drivertracker';
    }
};
AppComponent = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
        selector: 'app-root',
        template: __webpack_require__(/*! ./app.component.html */ "./src/app/app.component.html"),
        styles: [__webpack_require__(/*! ./app.component.sass */ "./src/app/app.component.sass")]
    })
], AppComponent);



/***/ }),

/***/ "./src/app/app.module.ts":
/*!*******************************!*\
  !*** ./src/app/app.module.ts ***!
  \*******************************/
/*! exports provided: AppModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppModule", function() { return AppModule; });
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/platform-browser */ "./node_modules/@angular/platform-browser/fesm2015/platform-browser.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm2015/http.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm2015/forms.js");
/* harmony import */ var _ng_bootstrap_ng_bootstrap__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @ng-bootstrap/ng-bootstrap */ "./node_modules/@ng-bootstrap/ng-bootstrap/fesm2015/ng-bootstrap.js");
/* harmony import */ var _app_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./app.component */ "./src/app/app.component.ts");
/* harmony import */ var _drivers_drivers_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./drivers/drivers.component */ "./src/app/drivers/drivers.component.ts");
/* harmony import */ var _legs_legs_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./legs/legs.component */ "./src/app/legs/legs.component.ts");
/* harmony import */ var _app_routing_module__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./app-routing.module */ "./src/app/app-routing.module.ts");
/* harmony import */ var _predictor_predictor_component__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./predictor/predictor.component */ "./src/app/predictor/predictor.component.ts");
/* harmony import */ var _pickup_predictor_pickup_predictor_component__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./pickup-predictor/pickup-predictor.component */ "./src/app/pickup-predictor/pickup-predictor.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};











let AppModule = class AppModule {
};
AppModule = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
        declarations: [
            _app_component__WEBPACK_IMPORTED_MODULE_5__["AppComponent"],
            _drivers_drivers_component__WEBPACK_IMPORTED_MODULE_6__["DriversComponent"],
            _legs_legs_component__WEBPACK_IMPORTED_MODULE_7__["LegsComponent"],
            _predictor_predictor_component__WEBPACK_IMPORTED_MODULE_9__["PredictorComponent"],
            _pickup_predictor_pickup_predictor_component__WEBPACK_IMPORTED_MODULE_10__["PickupPredictorComponent"]
        ],
        imports: [
            _angular_platform_browser__WEBPACK_IMPORTED_MODULE_0__["BrowserModule"],
            _angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClientModule"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_3__["FormsModule"],
            _app_routing_module__WEBPACK_IMPORTED_MODULE_8__["AppRoutingModule"],
            _ng_bootstrap_ng_bootstrap__WEBPACK_IMPORTED_MODULE_4__["NgbModule"].forRoot()
        ],
        providers: [],
        bootstrap: [_app_component__WEBPACK_IMPORTED_MODULE_5__["AppComponent"]]
    })
], AppModule);



/***/ }),

/***/ "./src/app/driver.service.ts":
/*!***********************************!*\
  !*** ./src/app/driver.service.ts ***!
  \***********************************/
/*! exports provided: DriverService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DriverService", function() { return DriverService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm2015/http.js");
/* harmony import */ var _leg_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./leg.service */ "./src/app/leg.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



const httpOptions = {
    headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"]({ 'Content-Type': 'application/json' })
};
let DriverService = class DriverService {
    constructor(http, legService) {
        this.http = http;
        this.legService = legService;
        this.driversUrl = '/api/driversapi';
    }
    getDrivers() {
        return this.http.get(this.driversUrl);
    }
    getDriver(id) {
        const url = `${this.driversUrl}/${id}`;
        return this.http.get(url);
    }
    updateDriver(driver) {
        const url = `${this.driversUrl}/${driver.driverID}`;
        return this.http.put(url, driver, httpOptions);
    }
    addDriver(driver) {
        const url = `${this.driversUrl}/new`;
        return this.http.post(url, driver, httpOptions);
    }
    deleteDriver(driver) {
        const id = typeof driver === 'number' ? driver : driver.driverID;
        const url = `${this.driversUrl}/${id}`;
        return this.http.delete(url, httpOptions);
    }
};
DriverService = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
        providedIn: 'root'
    }),
    __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"],
        _leg_service__WEBPACK_IMPORTED_MODULE_2__["LegService"]])
], DriverService);



/***/ }),

/***/ "./src/app/drivers/drivers.component.html":
/*!************************************************!*\
  !*** ./src/app/drivers/drivers.component.html ***!
  \************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<ng-template [ngIf]=\"companyStatistics != null\">\r\n<h2>Statistics</h2>\r\n<ul>\r\n    <li>{{companyStatistics.numOfDrivers}} driver<ng-template [ngIf]=\"companyStatistics.numOfDrivers != 1\">s</ng-template></li>\r\n    <li>{{companyStatistics.pickups}} passenger pickup<ng-template [ngIf]=\"companyStatistics.numOfPickups != 1\">s</ng-template></li>\r\n    <li>{{companyStatistics.milesDriven}} mile<ng-template [ngIf]=\"companyStatistics.milesDriven != 1\">s</ng-template> driven</li>\r\n    <li *ngIf=\"companyStatistics.averagePickupDelay != null\">Average pickup delay: \r\n    {{companyStatistics.averagePickupDelay}} minute<ng-template [ngIf]=\"companyStatistics.averagePickupDela != 1\">s</ng-template></li>\r\n    <li>Total fares: ${{companyStatistics.totalFares}}</li>\r\n    <li>Total fuel costs: ${{companyStatistics.totalCosts}}</li>\r\n    <li>Net profit: ${{companyStatistics.netProfit}}</li>\r\n</ul>\r\n</ng-template>\r\n\r\n<h2>Drivers</h2>\r\n<form (ngSubmit)=\"onSubmit()\" #driversForm=\"ngForm\">\r\n    <table class=\"table\">\r\n        <thead>\r\n            <tr>\r\n                <th>\r\n                    Name\r\n                </th>\r\n                <th>\r\n                    License Number\r\n                </th>\r\n                <th></th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n            <tr *ngFor=\"let driver of drivers\">\r\n                <td [ngSwitch]=\"editing\">\r\n                    <input type=\"text\" class=\"form-control\" id=\"name\" required name=\"name_{{driver.driverID}}\" [(ngModel)]=\"driver.name\"\r\n                           *ngSwitchCase=\"driver.driverID\" />\r\n                    <span *ngSwitchDefault>{{driver.name}}</span>\r\n                </td>\r\n                <td [ngSwitch]=\"editing\">\r\n                    <input type=\"text\" class=\"form-control\" id=\"licenseNumber\" required name=\"licenseNumber_{{driver.driverID}}\" [(ngModel)]=\"driver.licenseNumber\"\r\n                           *ngSwitchCase=\"driver.driverID\" />\r\n                    <span *ngSwitchDefault>{{driver.licenseNumber}}</span>\r\n                </td>\r\n                <td [ngSwitch]=\"editing\">\r\n                    <div class=\"btn-toolbar\" role=\"toolbar\" *ngSwitchCase=\"driver.driverID\">\r\n                        <button type=\"submit\" class=\"btn btn-success mr-2\" [disabled]=\"!driversForm.form.valid\">Save</button>\r\n                        <button type=\"button\" class=\"btn btn-secondary\" (click)=\"cancelEdit()\">Cancel</button>\r\n                    </div>\r\n                    <span class=\"btn-group\" role=\"group\" *ngSwitchDefault>\r\n                        <a class=\"btn btn-secondary\" href=\"/Drivers/Details/{{driver.driverID}}\">Details</a>\r\n                        <button type=\"button\" class=\"btn btn-secondary\" (click)=\"editClicked(driver)\">Edit</button>\r\n                        <button type=\"button\" class=\"btn btn-secondary\" (click)=\"showDeleteConfirm(content, driver)\">Delete</button>\r\n                        <a class=\"btn btn-secondary\" href=\"/Predictor/Index/{{driver.driverID}}\">Predictive Analytics</a>\r\n                    </span>\r\n                </td>\r\n            </tr>\r\n            <tr *ngIf=\"adding; else addLink\">\r\n                <td>\r\n                    <input class=\"form-control\" required #name />\r\n                </td>\r\n                <td>\r\n                    <input class=\"form-control\" required #licenseNumber />\r\n                </td>\r\n                <td>\r\n                    <button type=\"submit\" class=\"btn btn-success mr-2\" [disabled]=\"!driversForm.form.valid\" (click)=\"saveNew(name.value, licenseNumber.value); name.value=''; licenseNumber.value=''\">Save</button>\r\n                    <button type=\"button\" class=\"btn btn-secondary\" (click)=\"cancelAdd()\">Cancel</button>\r\n                </td>\r\n            </tr>\r\n            <ng-template #addLink>\r\n                <button type=\"button\" class=\"btn btn-success\" (click)=\"addNew()\">New Driver</button>\r\n            </ng-template>\r\n        </tbody>\r\n    </table>\r\n</form>\r\n<!-- delete confirm modal -->\r\n<ng-template #content let-modal>\r\n    <div class=\"modal-dialog\" role=\"document\">\r\n        <div class=\"modal-content\">\r\n            <div class=\"modal-header\">\r\n                <h5 class=\"modal-title\">Confirm Delete</h5>\r\n                <button type=\"button\" class=\"close\" aria-label=\"Close\" (click)=\"modal.dismiss('crossClicked')\">\r\n                    <span aria-hidden=\"true\">&times;</span>\r\n                </button>\r\n            </div>\r\n            <div class=\"modal-body\">\r\n                <p>Are you sure you want to delete driver {{toDelete.name}}?</p>\r\n            </div>\r\n            <div class=\"modal-footer\">\r\n                <button type=\"button\" class=\"btn btn-primary\" (click)=\"modal.close('deleteConfirmed')\">Delete</button>\r\n                <button type=\"button\" class=\"btn btn-secondary\" (click)=\"modal.close('deleteCancelled')\">Cancel</button>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</ng-template>"

/***/ }),

/***/ "./src/app/drivers/drivers.component.sass":
/*!************************************************!*\
  !*** ./src/app/drivers/drivers.component.sass ***!
  \************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJkcml2ZXJzL2RyaXZlcnMuY29tcG9uZW50LnNhc3MifQ== */"

/***/ }),

/***/ "./src/app/drivers/drivers.component.ts":
/*!**********************************************!*\
  !*** ./src/app/drivers/drivers.component.ts ***!
  \**********************************************/
/*! exports provided: DriversComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DriversComponent", function() { return DriversComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _ng_bootstrap_ng_bootstrap__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @ng-bootstrap/ng-bootstrap */ "./node_modules/@ng-bootstrap/ng-bootstrap/fesm2015/ng-bootstrap.js");
/* harmony import */ var _driver_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../driver.service */ "./src/app/driver.service.ts");
/* harmony import */ var _statistics_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../statistics.service */ "./src/app/statistics.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




const DELETE_CONFIRMED = "deleteConfirmed";
const DELETE_CANCELLED = "deleteCancelled";
const CROSS_CLICKED = "crossClicked";
let DriversComponent = class DriversComponent {
    constructor(driverService, statisticsService, modalService) {
        this.driverService = driverService;
        this.statisticsService = statisticsService;
        this.modalService = modalService;
        this.editing = 0;
        this.adding = false;
    }
    getDrivers() {
        this.driverService.getDrivers().subscribe(drivers => this.drivers = drivers);
    }
    getCompanyStatistics() {
        this.statisticsService.getCompanyStatistics()
            .subscribe(stats => this.companyStatistics = stats);
    }
    editClicked(driver) {
        this.cancelAdd();
        this.editing = driver.driverID;
    }
    cancelEdit() {
        this.driverService.getDriver(this.editing).subscribe(driver => {
            let dmem = this.drivers.find(d => d.driverID == this.editing);
            if (dmem !== null && driver !== null) {
                dmem.licenseNumber = driver.licenseNumber;
                dmem.name = driver.name;
                this.editing = 0;
            }
        });
    }
    addNew() {
        this.cancelEdit();
        this.adding = true;
    }
    cancelAdd() {
        this.adding = false;
    }
    onSubmit() {
        if (this.editing !== 0) {
            let driver = this.drivers.find(d => d.driverID == this.editing);
            this.driverService.updateDriver(driver).subscribe(x => this.editing = 0);
        }
    }
    saveNew(name, licenseNumber) {
        name = name.trim();
        if (!name) {
            return;
        }
        licenseNumber = licenseNumber.trim();
        if (!licenseNumber) {
            return;
        }
        this.driverService.addDriver({ name, licenseNumber })
            .subscribe(driver => {
            this.drivers.push(driver);
            this.getCompanyStatistics(); // update to reflect change
        });
    }
    showDeleteConfirm(content, driver) {
        this.toDelete = driver;
        this.modalService.open(content, { ariaLabelledBy: 'deleteConfirmLabel' })
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
    deleteDriver(driver) {
        this.driverService.deleteDriver(driver).subscribe(x => {
            var indexToDelete = this.drivers.indexOf(this.toDelete, 0);
            this.drivers.splice(indexToDelete, 1);
            this.toDelete = null;
            this.getCompanyStatistics(); // update to reflect change
        });
    }
    cancelDelete() {
        this.toDelete = null;
    }
    ngOnInit() {
        this.getDrivers();
        this.getCompanyStatistics();
    }
};
DriversComponent = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
        selector: 'app-drivers',
        template: __webpack_require__(/*! ./drivers.component.html */ "./src/app/drivers/drivers.component.html"),
        styles: [__webpack_require__(/*! ./drivers.component.sass */ "./src/app/drivers/drivers.component.sass")]
    }),
    __metadata("design:paramtypes", [_driver_service__WEBPACK_IMPORTED_MODULE_2__["DriverService"],
        _statistics_service__WEBPACK_IMPORTED_MODULE_3__["StatisticsService"],
        _ng_bootstrap_ng_bootstrap__WEBPACK_IMPORTED_MODULE_1__["NgbModal"]])
], DriversComponent);



/***/ }),

/***/ "./src/app/geocoding.service.ts":
/*!**************************************!*\
  !*** ./src/app/geocoding.service.ts ***!
  \**************************************/
/*! exports provided: GeocodingService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GeocodingService", function() { return GeocodingService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm2015/http.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


const httpOptions = {
    headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"]({ 'Content-Type': 'application/json' })
};
let GeocodingService = class GeocodingService {
    constructor(http) {
        this.http = http;
        this.geocodingUrl = "/api/geocoding";
    }
    getLegCoordinates(id) {
        const url = `${this.geocodingUrl}/${id}`;
        return this.http.get(url, httpOptions);
    }
    getAddressCoordinates(address) {
        const url = `${this.geocodingUrl}/direct/${address}`;
        return this.http.get(url, httpOptions);
    }
};
GeocodingService = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
        providedIn: 'root'
    }),
    __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"]])
], GeocodingService);



/***/ }),

/***/ "./src/app/leg.service.ts":
/*!********************************!*\
  !*** ./src/app/leg.service.ts ***!
  \********************************/
/*! exports provided: LegService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LegService", function() { return LegService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

let LegService = class LegService {
    constructor() { }
};
LegService = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
        providedIn: 'root'
    }),
    __metadata("design:paramtypes", [])
], LegService);



/***/ }),

/***/ "./src/app/legs/legs.component.html":
/*!******************************************!*\
  !*** ./src/app/legs/legs.component.html ***!
  \******************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<p>\n  legs works!\n</p>\n"

/***/ }),

/***/ "./src/app/legs/legs.component.sass":
/*!******************************************!*\
  !*** ./src/app/legs/legs.component.sass ***!
  \******************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJsZWdzL2xlZ3MuY29tcG9uZW50LnNhc3MifQ== */"

/***/ }),

/***/ "./src/app/legs/legs.component.ts":
/*!****************************************!*\
  !*** ./src/app/legs/legs.component.ts ***!
  \****************************************/
/*! exports provided: LegsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LegsComponent", function() { return LegsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

let LegsComponent = class LegsComponent {
    constructor() { }
    ngOnInit() {
    }
};
LegsComponent = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
        selector: 'app-legs',
        template: __webpack_require__(/*! ./legs.component.html */ "./src/app/legs/legs.component.html"),
        styles: [__webpack_require__(/*! ./legs.component.sass */ "./src/app/legs/legs.component.sass")]
    }),
    __metadata("design:paramtypes", [])
], LegsComponent);



/***/ }),

/***/ "./src/app/pickup-predictor.service.ts":
/*!*********************************************!*\
  !*** ./src/app/pickup-predictor.service.ts ***!
  \*********************************************/
/*! exports provided: PickupPredictorService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PickupPredictorService", function() { return PickupPredictorService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm2015/http.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


const httpOptions = {
    headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"]({ 'Content-Type': 'application/json' })
};
let PickupPredictorService = class PickupPredictorService {
    constructor(http) {
        this.http = http;
        this.analysisUrl = '/api/analysisapi';
    }
    getFareClassIntervals() {
        const url = `${this.analysisUrl}/fareclassintervals`;
        return this.http.get(url, httpOptions);
    }
    setFareClassIntervals(intervalBounds) {
        const url = `${this.analysisUrl}/fareclassintervals`;
        return this.http.put(url, intervalBounds, httpOptions);
    }
    getFareClassProbabilities(startCoords, endCoords, delay, duration, pickups, interval) {
        const url = `${this.analysisUrl}/fareclassprob/${startCoords[0]}/${startCoords[1]}/${endCoords[0]}/${endCoords[1]}/${delay}/${duration}/${pickups}/${interval}`;
        return this.http.get(url, httpOptions);
    }
    getPickupProbabilities(startCoords, endCoords, delay, duration, fare, interval) {
        const url = `${this.analysisUrl}/pickupprob/${startCoords[0]}/${startCoords[1]}/${endCoords[0]}/${endCoords[1]}/${delay}/${duration}/${fare}/${interval}`;
        return this.http.get(url, httpOptions);
    }
};
PickupPredictorService = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
        providedIn: 'root'
    }),
    __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"]])
], PickupPredictorService);



/***/ }),

/***/ "./src/app/pickup-predictor/pickup-predictor.component.html":
/*!******************************************************************!*\
  !*** ./src/app/pickup-predictor/pickup-predictor.component.html ***!
  \******************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<h2>Pickup predictions</h2>\n<p>\n    Predict probability of completing given leg and collecting fares.\n</p>\n\n<form (ngSubmit)=\"onSubmit()\" #pickupPredictionForm=\"ngForm\">\r\n    <h3>Leg information</h3>\r\n    <div class=\"m-5\">\r\n        <div class=\"form-group\">\r\n            <label for=\"startAddress\">Start address</label>\r\n            <input type=\"text\" name=\"startAddress\" required [(ngModel)]=\"startAddress\" class=\"form-control\" />\r\n        </div>\r\n        <div class=\"form-group\">\r\n            <label for=\"endAddress\">End address</label>\r\n            <input type=\"text\" name=\"endAddress\" required [(ngModel)]=\"endAddress\" class=\"form-control\" />\r\n        </div>\r\n        <div class=\"form-group\">\r\n            <label for=\"pickupDelay\">Pickup delay (min)</label>\r\n            <input type=\"number\" name=\"pickupDelay\" required [(ngModel)]=\"delay\" class=\"form-control\" />\r\n        </div>\r\n        <div class=\"form-group\">\r\n            <label for=\"duration\">Duration (min)</label>\r\n            <input type=\"number\" name=\"duration\" required [(ngModel)]=\"duration\" class=\"form-control\" />\r\n        </div>\r\n        <div class=\"form-group\">\r\n            <label for=\"interval\">Interval over which to predict probabilities (min)</label>\r\n            <input type=\"number\" name=\"interval\" required [(ngModel)]=\"interval\" class=\"form-control\" />\r\n        </div>\r\n    </div>\r\n    <h3>Fare class probabilities</h3>\r\n    <div class=\"m-5\">\r\n        <div class=\"form-group\">\r\n            <label for=\"pickups\">Number of pickups</label>\r\n            <input type=\"number\" name=\"pickups\" [(ngModel)]=\"pickups\" class=\"form-control\" />\r\n        </div>\r\n        <button type=\"submit\" class=\"btn btn-success\">Predict fare class probabilities</button>\r\n    </div>\r\n    <h3>Pickup probabilities</h3>\r\n    <div class=\"m-5\">\r\n        <div class=\"form-group\">\r\n            <label for=\"fare\">Fare</label>\r\n            <input type=\"number\" name=\"fare\" [(ngModel)]=\"fare\" class=\"form-control\" />\r\n        </div>\r\n        <button type=\"button\" class=\"btn btn-success\" (click)=\"predictPickupProbabilities()\">Predict pickup probabilities</button>\r\n    </div>\r\n</form>\n\n<h2>Results</h2>\n<ng-template [ngIf]=\"fareClassProbabilities != null\">\n    <h3>Fare class probabilities</h3>\n    <div class=\"row\" *ngFor=\"let i of fareClassIndices\">\n        <div class=\"col\">\n            <p class=\"pl-5\">${{i == 0 ? 0 : fareClassIntervalBoundaries[i-1]}} to ${{fareClassIntervalBoundaries[i]}}:</p>\n        </div>\n        <div class=\"col\">\n            <p class=\"pr-5\">{{fareClassProbabilities[i] * 100}}%</p>\n        </div>\n    </div>\n    <div class=\"row\" *ngIf=\"fareClassIntervalBoundaries.length > 0\">\r\n        <div class=\"col\">\r\n            <p class=\"pl-5\">${{fareClassIntervalBoundaries[fareClassIntervalBoundaries.length - 1]}}+:</p>\r\n        </div>\r\n        <div class=\"col\">\r\n            <p class=\"pr-5\">{{fareClassProbabilities[fareClassIntervalBoundaries.length] * 100}}%</p>\r\n        </div>\r\n    </div>\n</ng-template>\n<ng-template [ngIf]=\"pickupProbabilities != null\">\r\n    <h3>Pickup probabilities</h3>\r\n    <div class=\"row\" *ngFor=\"let i of pickupProbabilityIndices\">\r\n        <div class=\"col\">\r\n            <p class=\"pl-5\" *ngIf=\"i == 0\">\n                Probability of failure to collect:\n            </p>\n            <p class=\"pl-5\" *ngIf=\"i > 0\">\r\n                Probability of collecting {{i}} passenger<ng-template [ngIf]=\"i != 1\">s</ng-template>:\r\n            </p>\r\n        </div>\r\n        <div class=\"col\">\r\n            <p class=\"pr-5\">{{pickupProbabilities[i] * 100}}%</p>\r\n        </div>\r\n    </div>\r\n</ng-template>"

/***/ }),

/***/ "./src/app/pickup-predictor/pickup-predictor.component.scss":
/*!******************************************************************!*\
  !*** ./src/app/pickup-predictor/pickup-predictor.component.scss ***!
  \******************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJwaWNrdXAtcHJlZGljdG9yL3BpY2t1cC1wcmVkaWN0b3IuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/pickup-predictor/pickup-predictor.component.ts":
/*!****************************************************************!*\
  !*** ./src/app/pickup-predictor/pickup-predictor.component.ts ***!
  \****************************************************************/
/*! exports provided: PickupPredictorComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PickupPredictorComponent", function() { return PickupPredictorComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm2015/router.js");
/* harmony import */ var _pickup_predictor_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../pickup-predictor.service */ "./src/app/pickup-predictor.service.ts");
/* harmony import */ var _geocoding_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../geocoding.service */ "./src/app/geocoding.service.ts");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm2015/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm2015/operators/index.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






let PickupPredictorComponent = class PickupPredictorComponent {
    constructor(pickupPredictorService, geocodingService, aRoute) {
        this.pickupPredictorService = pickupPredictorService;
        this.geocodingService = geocodingService;
        this.aRoute = aRoute;
    }
    geocodeInputAddresses(geoDependent) {
        const startCoords$ = this.geocodingService.getAddressCoordinates(this.startAddress);
        const endCoords$ = this.geocodingService.getAddressCoordinates(this.endAddress);
        return Object(rxjs__WEBPACK_IMPORTED_MODULE_4__["combineLatest"])(startCoords$, endCoords$, (startCoords, endCoords) => ({ startCoords, endCoords }))
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_5__["switchMap"])(pair => {
            this.startCoords = pair.startCoords;
            this.endCoords = pair.endCoords;
            return geoDependent(this.startCoords, this.endCoords);
        }));
    }
    onSubmit() {
        this.geocodeInputAddresses((s, e) => this.pickupPredictorService.getFareClassProbabilities(s, e, this.delay, this.duration, this.pickups, this.interval))
            .subscribe(probs => this.fareClassProbabilities = probs);
    }
    predictPickupProbabilities() {
        this.geocodeInputAddresses((s, e) => this.pickupPredictorService.getPickupProbabilities(s, e, this.delay, this.duration, this.fare, this.interval))
            .subscribe(probs => {
            this.pickupProbabilities = probs;
            this.pickupProbabilityIndices = (new Array(probs.length)).fill(0).map((x, i) => i);
        });
    }
    ngOnInit() {
        this.pickupPredictorService.getFareClassIntervals()
            .subscribe(bounds => this.fareClassIntervalBoundaries = bounds);
        this.fareClassIndices = (new Array(this.fareClassIntervalBoundaries.length))
            .fill(0).map((x, i) => i);
    }
};
PickupPredictorComponent = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
        selector: 'app-pickup-predictor',
        template: __webpack_require__(/*! ./pickup-predictor.component.html */ "./src/app/pickup-predictor/pickup-predictor.component.html"),
        styles: [__webpack_require__(/*! ./pickup-predictor.component.scss */ "./src/app/pickup-predictor/pickup-predictor.component.scss")]
    }),
    __metadata("design:paramtypes", [_pickup_predictor_service__WEBPACK_IMPORTED_MODULE_2__["PickupPredictorService"],
        _geocoding_service__WEBPACK_IMPORTED_MODULE_3__["GeocodingService"],
        _angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"]])
], PickupPredictorComponent);



/***/ }),

/***/ "./src/app/predictor.service.ts":
/*!**************************************!*\
  !*** ./src/app/predictor.service.ts ***!
  \**************************************/
/*! exports provided: PredictorService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PredictorService", function() { return PredictorService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm2015/http.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


const httpOptions = {
    headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"]({ 'Content-Type': 'application/json' })
};
let PredictorService = class PredictorService {
    constructor(http) {
        this.http = http;
        this.analysisUrl = '/api/analysisapi';
    }
    getRidershipProbabilities(driverID, delay, duration, fare) {
        const url = `${this.analysisUrl}/multipickupprob/${driverID}/${delay}/${duration}/${fare}`;
        return this.http.get(url, httpOptions);
    }
};
PredictorService = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
        providedIn: 'root'
    }),
    __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"]])
], PredictorService);



/***/ }),

/***/ "./src/app/predictor/predictor.component.html":
/*!****************************************************!*\
  !*** ./src/app/predictor/predictor.component.html ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<h2>Ridership Prediction for Driver: {{driver.name}}</h2>\n\n<p>\n    Predict how many riders are likely <em>if a pickup occurs</em>.\n</p>\n\n<form (ngSubmit)=\"onSubmit()\" #riderPredictionForm=\"ngForm\">\n    <div class=\"form-group\">\n        <label for=\"delay\">Anticipated delay (min)</label>\n        <input type=\"number\" name=\"delay\" required [(ngModel)]=\"delay\" class=\"form-control\" />\n    </div>\n    <div class=\"form-group\">\r\n        <label for=\"duration\">Leg duration (min)</label>\r\n        <input type=\"number\" name=\"duration\" required [(ngModel)]=\"duration\" class=\"form-control\" />\r\n    </div>\n    <div class=\"form-group\">\r\n        <label for=\"fare\">Fare ($)</label>\r\n        <input type=\"number\" name=\"fare\" required [(ngModel)]=\"fare\" class=\"form-control\" />\r\n    </div>\n    <button type=\"submit\" class=\"btn btn-success\" [disabled]=\"!riderPredictionForm.form.valid\">Predict</button>\n</form>\n\n<ng-template [ngIf]=\"ridershipProbabilities != null\">\r\n    <div class=\"row\" *ngFor=\"let i of ridershipProbabilityIndices\">\r\n        <div class=\"col\">\r\n            <p class=\"pl-5\">Probability of {{i+2}}<ng-template [ngIf]=\"i == 3\">+</ng-template> riders:</p>\r\n        </div>\r\n        <div class=\"col\">\r\n            <p class=\"pr-5\">{{ridershipProbabilities[i] * 100}}%</p>\r\n        </div>\r\n    </div>\r\n</ng-template>"

/***/ }),

/***/ "./src/app/predictor/predictor.component.sass":
/*!****************************************************!*\
  !*** ./src/app/predictor/predictor.component.sass ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJwcmVkaWN0b3IvcHJlZGljdG9yLmNvbXBvbmVudC5zYXNzIn0= */"

/***/ }),

/***/ "./src/app/predictor/predictor.component.ts":
/*!**************************************************!*\
  !*** ./src/app/predictor/predictor.component.ts ***!
  \**************************************************/
/*! exports provided: PredictorComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PredictorComponent", function() { return PredictorComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm2015/router.js");
/* harmony import */ var _predictor_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../predictor.service */ "./src/app/predictor.service.ts");
/* harmony import */ var _driver_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../driver.service */ "./src/app/driver.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




let PredictorComponent = class PredictorComponent {
    constructor(predictorService, driverService, aRoute) {
        this.predictorService = predictorService;
        this.driverService = driverService;
        this.aRoute = aRoute;
        this.aRoute.params.subscribe(p => this.getDriver(p['id']));
        this.ridershipProbabilityIndices = (new Array(4)).fill(0).map((x, i) => i);
    }
    getDriver(id) {
        this.driverService.getDriver(id).subscribe(driver => this.driver = driver);
    }
    onSubmit() {
        this.predictorService.getRidershipProbabilities(this.driver.driverID, this.delay, this.duration, this.fare)
            .subscribe(probs => this.ridershipProbabilities = probs);
    }
    ngOnInit() {
    }
};
PredictorComponent = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
        selector: 'app-predictor',
        template: __webpack_require__(/*! ./predictor.component.html */ "./src/app/predictor/predictor.component.html"),
        styles: [__webpack_require__(/*! ./predictor.component.sass */ "./src/app/predictor/predictor.component.sass")]
    }),
    __metadata("design:paramtypes", [_predictor_service__WEBPACK_IMPORTED_MODULE_2__["PredictorService"],
        _driver_service__WEBPACK_IMPORTED_MODULE_3__["DriverService"],
        _angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"]])
], PredictorComponent);



/***/ }),

/***/ "./src/app/statistics.service.ts":
/*!***************************************!*\
  !*** ./src/app/statistics.service.ts ***!
  \***************************************/
/*! exports provided: StatisticsService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "StatisticsService", function() { return StatisticsService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm2015/http.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


const httpOptions = {
    headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"]({ 'Content-Type': 'application/json' })
};
let StatisticsService = class StatisticsService {
    constructor(httpClient) {
        this.httpClient = httpClient;
        this.analysisUrl = '/api/analysisapi';
    }
    getCompanyStatistics() {
        const url = `${this.analysisUrl}/company`;
        return this.httpClient.get(url);
    }
};
StatisticsService = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
        providedIn: 'root'
    }),
    __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"]])
], StatisticsService);



/***/ }),

/***/ "./src/environments/environment.ts":
/*!*****************************************!*\
  !*** ./src/environments/environment.ts ***!
  \*****************************************/
/*! exports provided: environment */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "environment", function() { return environment; });
// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
const environment = {
    production: false
};
/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.


/***/ }),

/***/ "./src/main.ts":
/*!*********************!*\
  !*** ./src/main.ts ***!
  \*********************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/platform-browser-dynamic */ "./node_modules/@angular/platform-browser-dynamic/fesm2015/platform-browser-dynamic.js");
/* harmony import */ var _app_app_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./app/app.module */ "./src/app/app.module.ts");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./environments/environment */ "./src/environments/environment.ts");




if (_environments_environment__WEBPACK_IMPORTED_MODULE_3__["environment"].production) {
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["enableProdMode"])();
}
Object(_angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__["platformBrowserDynamic"])().bootstrapModule(_app_app_module__WEBPACK_IMPORTED_MODULE_2__["AppModule"])
    .catch(err => console.error(err));


/***/ }),

/***/ 0:
/*!***************************!*\
  !*** multi ./src/main.ts ***!
  \***************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! /Users/davidnisson/Projects/DriverTracker/DriverTracker/src/main.ts */"./src/main.ts");


/***/ })

},[[0,"runtime","vendor"]]]);
//# sourceMappingURL=main.js.map