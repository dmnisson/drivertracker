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
/* harmony import */ var _driver_details_driver_details_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./driver-details/driver-details.component */ "./src/app/driver-details/driver-details.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};






const routes = [
    { path: 'Drivers', component: _drivers_drivers_component__WEBPACK_IMPORTED_MODULE_2__["DriversComponent"] },
    { path: 'Predictor/Index/:id', component: _predictor_predictor_component__WEBPACK_IMPORTED_MODULE_3__["PredictorComponent"] },
    { path: 'Drivers/Details/:id', component: _driver_details_driver_details_component__WEBPACK_IMPORTED_MODULE_5__["DriverDetailsComponent"] },
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

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2FwcC5jb21wb25lbnQuc2FzcyJ9 */"

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
/* harmony import */ var _driver_details_driver_details_component__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./driver-details/driver-details.component */ "./src/app/driver-details/driver-details.component.ts");
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
            _pickup_predictor_pickup_predictor_component__WEBPACK_IMPORTED_MODULE_10__["PickupPredictorComponent"],
            _driver_details_driver_details_component__WEBPACK_IMPORTED_MODULE_11__["DriverDetailsComponent"],
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

/***/ "./src/app/auth.service.ts":
/*!*********************************!*\
  !*** ./src/app/auth.service.ts ***!
  \*********************************/
/*! exports provided: AuthService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthService", function() { return AuthService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm2015/http.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm2015/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm2015/operators/index.js");
/* harmony import */ var _login_model__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./login-model */ "./src/app/login-model.ts");
/* harmony import */ var jwt_decode__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! jwt-decode */ "./node_modules/jwt-decode/lib/index.js");
/* harmony import */ var jwt_decode__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(jwt_decode__WEBPACK_IMPORTED_MODULE_5__);
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






const jsonHeader = {
    headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"]({ 'Content-Type': 'application/json' })
};
let AuthService = class AuthService {
    constructor(http) {
        this.http = http;
        this.authUrl = '/api/account';
        this.token = new rxjs__WEBPACK_IMPORTED_MODULE_2__["BehaviorSubject"](null);
        this.tokenExpired = new rxjs__WEBPACK_IMPORTED_MODULE_2__["BehaviorSubject"](false);
    }
    makeToken(email, password) {
        const url = this.authUrl + '/maketoken';
        this.http.post(url, new _login_model__WEBPACK_IMPORTED_MODULE_4__["LoginModel"](email, password), Object.assign(jsonHeader, { responseType: 'text' }))
            .subscribe(t => this.updateToken(t));
    }
    makeSessionUserToken(force = false) {
        const url = this.authUrl + '/makesessionusertoken';
        this.token.asObservable().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["filter"])(token => force || token == null), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["switchMap"])(token => this.http.post(url, "", { responseType: 'text' })))
            .subscribe(t => this.updateToken(t));
    }
    refreshToken() {
        const url = this.authUrl + '/refreshtoken';
        this.authHeader().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["map"])(ah => new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"](ah)), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["switchMap"])(ah => this.http.post(url, "", { headers: ah })))
            .subscribe(t => this.updateToken(t));
    }
    getCurrentToken() {
        return this.token.asObservable().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["skipWhile"])(t => t == null));
    }
    isTokenExpired() {
        return this.tokenExpired.asObservable();
    }
    // update the information stored in the class
    updateToken(token) {
        this.token.next(token);
        this.tokenExpired.next(false);
        let expDate = jwt_decode__WEBPACK_IMPORTED_MODULE_5__(token).exp;
        Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["timer"])(expDate - 15 * 60000).subscribe(obj => this.refreshToken());
    }
    authHeader() {
        return this.getCurrentToken().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["map"])(t => { return { 'Authorization': 'Bearer ' + t }; }));
    }
};
AuthService = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
        providedIn: 'root'
    }),
    __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"]])
], AuthService);



/***/ }),

/***/ "./src/app/driver-details/driver-details.component.html":
/*!**************************************************************!*\
  !*** ./src/app/driver-details/driver-details.component.html ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<ng-template [ngIf]=\"driverStatistics != null\">\r\n    <div class=\"card w-md-50 mb-2\">\r\n        <div class=\"card-body\">\r\n            <h5 class=\"card-title\">Summary</h5>\r\n        </div>\r\n        <ul class=\"list-group list-group-flush\">\r\n            <li class=\"list-group-item\">Pickups: {{driverStatistics.pickups}}</li>\r\n            <li class=\"list-group-item\">Miles driven: {{driverStatistics.milesDriven}}</li>\r\n            <li class=\"list-group-item\" *ngIf=\"driverStatistics.averagePickupDelay != null\">Average pickup delay: {{driverStatistics.averagePickupDelay}}</li>\r\n            <li class=\"list-group-item\">Total fares: {{driverStatistics.totalFares}}</li>\r\n            <li class=\"list-group-item\">Total fuel costs: {{driverStatistics.totalCosts}}</li>\r\n        </ul>\r\n    </div>\r\n</ng-template>\r\n\r\n<ng-template [ngIf]=\"driver != null\">\r\n    <div class=\"card w-100 mb-2\">\r\n        <div class=\"card-body\">\r\n            <div class=\"d-flex justify-content-between align-items-center\">\r\n                <h5 class=\"card-title d-flex-inline\">Details</h5>\r\n                <button class=\"btn btn-primary d-flex-inline\" (click)=\"clickEditDriverDetails()\" [disabled]=\"editingDriverDetails\">\r\n                    <i class=\"fas fa-edit\"></i><span class=\"sr-only\">Edit</span>\r\n                </button>\r\n            </div>\r\n            <ng-template [ngIf]=\"editingDriverDetails\">\r\n                <form (ngSubmit)=\"onSubmitDriverDetails()\" #driverDetailsForm=\"ngForm\">\r\n                    <dl class=\"row\">\r\n                        <dt class=\"col-sm-5 card-text\">\r\n                            Name\r\n                        </dt>\r\n                        <dd class=\"col-sm-5 card-text\">\r\n                            <input type=\"text\" class=\"form-control\" id=\"name\" required name=\"name\" [(ngModel)]=\"driver.name\" />\r\n                        </dd>\r\n                        <dt class=\"col-sm-5 card-text\">\r\n                            License Number\r\n                        </dt>\r\n                        <dd class=\"col-sm-5 card-text\">\r\n                            <input type=\"text\" class=\"form-control\" id=\"licenseNumber\" required name=\"licenseNumber\" [(ngModel)]=\"driver.licenseNumber\" />\r\n                        </dd>\r\n                    </dl>\r\n                    <button type=\"submit\" class=\"btn btn-primary mr-2\" [disabled]=\"!driverDetailsForm.form.valid\">Save</button>\r\n                    <button type=\"button\" class=\"btn btn-secondary\" (click)=\"cancelEditDriverDetails()\">Cancel</button>\r\n                </form>\r\n            </ng-template>\r\n            <ng-template [ngIf]=\"!editingDriverDetails\">\r\n                <dl class=\"row\">\r\n                    <dt class=\"col-sm-5 card-text\">\r\n                        Name\r\n                    </dt>\r\n                    <dd class=\"col-sm-5 card-text\">\r\n                        {{driver.name}}\r\n                    </dd>\r\n                    <dt class=\"col-sm-5 card-text\">\r\n                        License Number\r\n                    </dt>\r\n                    <dd class=\"col-sm-5 card-text\">\r\n                        {{driver.licenseNumber}}\r\n                    </dd>\r\n                </dl>\r\n            </ng-template>\r\n        </div>\r\n    </div>\r\n</ng-template>\r\n\r\n<div class=\"card w-100 mb-2\">\r\n\r\n    <div class=\"card-body\">\r\n        <h5 class=\"card-title\">Legs</h5>\r\n        <ng-template [ngIf]=\"legs != null && legs.length > 0\">\r\n            <div class=\"table-responsive\">\r\n                <table class=\"table\">\r\n                    <thead>\r\n                        <tr>\r\n                            <th>\r\n                                Start Address\r\n                            </th>\r\n                            <th>\r\n                                Requested Pickup Time\r\n                            </th>\r\n                            <th>\r\n                                Actual Start Time\r\n                            </th>\r\n                            <th>\r\n                                Destination Address\r\n                            </th>\r\n                            <th>\r\n                                Arrival Time\r\n                            </th>\r\n                            <th class=\"d-none d-md-table-cell\">\r\n                                Distance\r\n                            </th>\r\n                            <th>\r\n                                Fare\r\n                            </th>\r\n                            <th class=\"d-none d-md-table-cell\">\r\n                                Passengers Aboard\r\n                            </th>\r\n                            <th>\r\n                                Pickups\r\n                            </th>\r\n                            <th></th>\r\n                        </tr>\r\n                    </thead>\r\n                    <tbody>\r\n                        <tr *ngFor=\"let leg of legs\">\r\n                            <td>\r\n                                {{leg.startAddress}}\r\n                            </td>\r\n                            <td>\r\n                                {{leg.pickupRequestTime | date:'short'}}\r\n                            </td>\r\n                            <td>\r\n                                {{leg.startTime | date:'short'}}\r\n                            </td>\r\n                            <td>\r\n                                {{leg.destinationAddress}}\r\n                            </td>\r\n                            <td>\r\n                                {{leg.arrivalTime | date:'short'}}\r\n                            </td>\r\n                            <td class=\"d-none d-md-table-cell\">\r\n                                {{leg.distance}}\r\n                            </td>\r\n                            <td>\r\n                                {{leg.fare}}\r\n                            </td>\r\n                            <td class=\"d-none d-md-table-cell\">\r\n                                {{leg.numOfPassengersAboard}}\r\n                            </td>\r\n                            <td>\r\n                                {{leg.numOfPassengersPickedUp}}\r\n                            </td>\r\n                            <td>\r\n                                <span class=\"btn-group\" role=\"group\">\r\n                                    <button type=\"button\" class=\"btn btn-secondary\" (click)=\"showLegDetails(leg, legDetails)\">Details</button>\r\n                                    <button type=\"button\" class=\"btn btn-secondary\" (click)=\"showDeleteConfirm(leg, deleteConfirm)\">Delete</button>\r\n                                </span>\r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td colspan=\"10\">\r\n                                <button type=\"button\" class=\"btn btn-primary\" (click)=\"showLegDetails(null, legDetails)\">New Leg</button>\r\n                            </td>\r\n                        </tr>\r\n                    </tbody>\r\n                </table>\r\n            </div>\r\n        </ng-template>\r\n        <ng-template [ngIf]=\"legs == null || legs.length == 0\">\r\n            <p class=\"card-text\">No legs have been entered.</p>\r\n        </ng-template>\r\n    </div>\r\n</div>\r\n\r\n<div>\r\n    <a class=\"btn btn-default\" href=\"/Drivers/\">Back to Drivers List</a>\r\n</div>\r\n\r\n<ng-template #legDetails let-modal>\r\n    <form (ngSubmit)=\"onSubmitLegDetails()\" #legDetailsForm=\"ngForm\">\r\n            <div class=\"modal-header\">\r\n                <h2>\r\n                    Leg Details\r\n                    <button type=\"button\" class=\"btn btn-primary\" (click)=\"editShowingLeg()\">\r\n                        <i class=\"fas fa-edit\"></i><span class=\"sr-only\">Edit</span>\r\n                    </button>\r\n                </h2>\r\n                <button type=\"button\" class=\"close\" aria-label=\"Close\" (click)=\"modal.dismiss('legDetailsCrossClick')\">\r\n                    <span aria-hidden=\"true\">&times;</span>\r\n                </button>\r\n            </div>\r\n            <div class=\"modal-body\">\r\n                <div>\r\n                    <dl class=\"row\">\r\n                        <dt class=\"col-sm-5\">\r\n                            Start Address\r\n                        </dt>\r\n                        <dd class=\"col-sm-5\">\r\n                            <input class=\"form-control\" type=\"text\" id=\"startAddress\" name=\"startAddress\" required [(ngModel)]=\"showingLeg.startAddress\"\r\n                                   *ngIf=\"editingLegDetails; else readOnlyStartAddress\" />\r\n                            <ng-template #readOnlyStartAddress>{{showingLeg.startAddress}}</ng-template>\r\n                        </dd>\r\n                        <dt class=\"col-sm-5\">\r\n                            Pickup Request Time\r\n                        </dt>\r\n                        <dd class=\"col-sm-5\">\r\n                            <input class=\"form-control\" type=\"text\" id=\"pickupRequestTime\" name=\"pickupRequestTime\" [(ngModel)]=\"showingLeg.pickupRequestTime\"\r\n                                   *ngIf=\"editingLegDetails; else readOnlyPickupRequestTime\" />\r\n                            <ng-template #readOnlyPickupRequestTime>{{showingLeg.pickupRequestTime | date:'short'}}</ng-template>\r\n                        </dd>\r\n                        <dt class=\"col-sm-5\">\r\n                            Start Time\r\n                        </dt>\r\n                        <dd class=\"col-sm-5\">\r\n                            <input class=\"form-control\" type=\"text\" id=\"startTime\" name=\"startTime\" required [(ngModel)]=\"showingLeg.startTime\"\r\n                                   *ngIf=\"editingLegDetails; else startTime\" />\r\n                            <ng-template #readOnlyStartTime>{{showingLeg.startTime | date:'short'}}</ng-template>\r\n                        </dd>\r\n                        <dt class=\"col-sm-5\">\r\n                            Destination Address\r\n                        </dt>\r\n                        <dd class=\"col-sm-5\">\r\n                            <input class=\"form-control\" type=\"text\" id=\"destinationAddress\" name=\"destinationAddress\" required [(ngModel)]=\"showingLeg.destinationAddress\"\r\n                                   *ngIf=\"editingLegDetails; else readOnlyDestinationAddress\" />\r\n                            <ng-template #readOnlyDestinationAddress>{{showingLeg.destinationAddress}}</ng-template>\r\n                        </dd>\r\n                        <dt class=\"col-sm-5\">\r\n                            Arrival Time\r\n                        </dt>\r\n                        <dd class=\"col-sm-5\">\r\n                            <input class=\"form-control\" type=\"text\" id=\"arrivalTime\" name=\"arrivalTime\" required [(ngModel)]=\"showingLeg.arrivalTime\"\r\n                                   *ngIf=\"editingLegDetails; else readOnlyArrivalTime\" />\r\n                            <ng-template #readOnlyArrivalTime>{{showingLeg.arrivalTime | date:'short'}}</ng-template>\r\n                        </dd>\r\n                        <dt class=\"col-sm-5\">\r\n                            Distance\r\n                        </dt>\r\n                        <dd class=\"col-sm-5\">\r\n                            <input class=\"form-control\" type=\"number\" id=\"distance\" name=\"distance\" required [(ngModel)]=\"showingLeg.distance\"\r\n                                   *ngIf=\"editingLegDetails; else readOnlyDistance\" />\r\n                            <ng-template #readOnlyDistance>{{showingLeg.distance}}</ng-template>\r\n                        </dd>\r\n                        <dt class=\"col-sm-5\">\r\n                            Fare\r\n                        </dt>\r\n                        <dd class=\"col-sm-5\">\r\n                            <input class=\"form-control\" type=\"number\" id=\"fare\" name=\"fare\" required [(ngModel)]=\"showingLeg.fare\"\r\n                                   *ngIf=\"editingLegDetails; else readOnlyFare\" />\r\n                            <ng-template #readOnlyFare>{{showingLeg.fare}}</ng-template>\r\n                        </dd>\r\n                        <dt class=\"col-sm-5\">\r\n                            Number of Passengers Aboard\r\n                        </dt>\r\n                        <dd class=\"col-sm-5\">\r\n                            <input class=\"form-control\" type=\"number\" id=\"numOfPassengersAboard\" name=\"numOfPassengersAboard\" required [(ngModel)]=\"showingLeg.numOfPassengersAboard\"\r\n                                   *ngIf=\"editingLegDetails; else readOnlyNumOfPassengersAboard\" />\r\n                            <ng-template #readOnlyNumOfPassengersAboard>{{showingLeg.numOfPassengersAboard}}</ng-template>\r\n                        </dd>\r\n                        <dt class=\"col-sm-5\">\r\n                            Number of Passengers Picked Up\r\n                        </dt>\r\n                        <dd class=\"col-sm-5\">\r\n                            <input class=\"form-control\" type=\"number\" id=\"numOfPassengersPickedUp\" name=\"numOfPassengersPickedUp\" required [(ngModel)]=\"showingLeg.numOfPassengersPickedUp\"\r\n                                   *ngIf=\"editingLegDetails; else readOnlyNumOfPassengersPickedUp\" />\r\n                            <ng-template #readOnlyNumOfPassengersPickedUp>{{showingLeg.numOfPassengersPickedUp}}</ng-template>\r\n                        </dd>\r\n                        <dt class=\"col-sm-5\">\r\n                            Fuel Cost\r\n                        </dt>\r\n                        <dd class=\"col-sm-5\">\r\n                            <input class=\"form-control\" type=\"number\" id=\"fuelCost\" name=\"fuelCost\" required [(ngModel)]=\"showingLeg.fuelCost\"\r\n                                   *ngIf=\"editingLegDetails; else readOnlyFuelCost\" />\r\n                            <ng-template #readOnlyFuelCost>{{showingLeg.fuelCost}}</ng-template>\r\n                        </dd>\r\n                    </dl>\r\n                </div>\r\n            </div>\r\n            <div class=\"modal-footer\">\r\n                <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"!legDetailsForm.form.valid\" *ngIf=\"editingLegDetails\" (click)=\"modal.close('legDetailsSaveClick')\">\r\n                    Save\r\n                </button>\r\n                <button type=\"button\" class=\"btn btn-secondary\" (click)=\"modal.close('legDetailsCloseClick')\">Close</button>\r\n            </div>\r\n    </form>\r\n</ng-template>\r\n\r\n<ng-template #deleteConfirm let-modal>\r\n    <div class=\"modal-header\">\r\n        <h2>Confirm</h2>\r\n        <button type=\"button\" class=\"close\" aria-label=\"Close\" (click)=\"modal.dismiss('legDeleteConfirmCrossClick')\">\r\n            <span aria-hidden=\"true\">&times;</span>\r\n        </button>\r\n    </div>\r\n    <div class=\"modal-body\">\r\n        Are you sure you want to delete this leg?\r\n    </div>\r\n    <div class=\"modal-footer\">\r\n        <button type=\"button\" class=\"btn btn-danger mr-2\" (click)=\"modal.close('legDeleteConfirmClick')\">\r\n            Delete\r\n        </button>\r\n        <button type=\"button\" class=\"btn btn-secondary\" (click)=\"modal.close('legDeleteCancelClick')\">\r\n            Cancel\r\n        </button>\r\n    </div>\r\n</ng-template>"

/***/ }),

/***/ "./src/app/driver-details/driver-details.component.sass":
/*!**************************************************************!*\
  !*** ./src/app/driver-details/driver-details.component.sass ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2RyaXZlci1kZXRhaWxzL2RyaXZlci1kZXRhaWxzLmNvbXBvbmVudC5zYXNzIn0= */"

/***/ }),

/***/ "./src/app/driver-details/driver-details.component.ts":
/*!************************************************************!*\
  !*** ./src/app/driver-details/driver-details.component.ts ***!
  \************************************************************/
/*! exports provided: DriverDetailsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DriverDetailsComponent", function() { return DriverDetailsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _ng_bootstrap_ng_bootstrap__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @ng-bootstrap/ng-bootstrap */ "./node_modules/@ng-bootstrap/ng-bootstrap/fesm2015/ng-bootstrap.js");
/* harmony import */ var _leg__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../leg */ "./src/app/leg.ts");
/* harmony import */ var _driver_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../driver.service */ "./src/app/driver.service.ts");
/* harmony import */ var _leg_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../leg.service */ "./src/app/leg.service.ts");
/* harmony import */ var _statistics_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../statistics.service */ "./src/app/statistics.service.ts");
/* harmony import */ var _auth_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../auth.service */ "./src/app/auth.service.ts");
/* harmony import */ var _node_modules_angular_router__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../../../node_modules/@angular/router */ "./node_modules/@angular/router/fesm2015/router.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};








const LEG_DETAILS_CROSS_CLICK = 'legDetailsCrossClick';
const LEG_DETAILS_SAVE_CLICK = 'legDetailsSaveClick';
const LEG_DETAILS_CLOSE_CLICK = 'legDetailsCloseClick';
const LEG_DELETE_CONFIRM_CROSS_CLICK = 'legDeleteConfirmCrossClick';
const LEG_DELETE_CONFIRM_CLICK = 'legDeleteConfirmClick';
const LEG_DELETE_CANCEL_CLICK = 'legDeleteCancelClick';
let DriverDetailsComponent = class DriverDetailsComponent {
    constructor(driverService, legService, statisticsService, authService, aRoute, ngbModal) {
        this.driverService = driverService;
        this.legService = legService;
        this.statisticsService = statisticsService;
        this.authService = authService;
        this.aRoute = aRoute;
        this.ngbModal = ngbModal;
        this.editingDriverDetails = false;
        this.editingLegDetails = false;
        this.addingLeg = false;
        this.aRoute.params.subscribe(p => this.loadDriver(p['id']));
    }
    ngOnInit() {
        this.authService.makeSessionUserToken();
    }
    loadDriver(id) {
        this.driverService.getDriver(id).
            subscribe(d => {
            this.driver = d;
            this.getDriverStatistics();
            this.loadLegs();
        });
    }
    getDriverStatistics() {
        this.statisticsService.getDriverStatistics(this.driver.driverID)
            .subscribe(s => { this.driverStatistics = s; });
    }
    loadLegs() {
        this.legService.getLegs(this.driver.driverID)
            .subscribe(a => { this.legs = a; });
    }
    clickEditDriverDetails() {
        this.editingDriverDetails = true;
    }
    cancelEditDriverDetails() {
        this.driverService.getDriver(this.driver.driverID)
            .subscribe(drv => { this.driver = drv; this.editingDriverDetails = false; });
    }
    onSubmitDriverDetails() {
        if (this.editingDriverDetails) {
            this.driverService.updateDriver(this.driver)
                .subscribe(x => { this.editingDriverDetails = false; });
        }
    }
    showLegDetails(leg, legDetails) {
        if (leg == null) {
            this.showingLeg = new _leg__WEBPACK_IMPORTED_MODULE_2__["Leg"]();
            this.showingLeg.driverID = this.driver.driverID;
            this.addingLeg = true;
            this.editingLegDetails = true;
        }
        else {
            this.showingLeg = leg;
            this.addingLeg = false;
        }
        this.ngbModal.open(legDetails, { ariaLabelledBy: 'legDetailsLabel' }).result
            .then((result) => {
            if (result == LEG_DETAILS_SAVE_CLICK) {
                console.log(result);
                this.onSubmitLegDetails();
            }
            else {
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
            var cuObs;
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
    showDeleteConfirm(leg, deleteConfirm) {
        this.legToDelete = leg;
        this.ngbModal.open(deleteConfirm, { ariaLabelledBy: 'legDeleteConfirmLabel' }).result
            .then((result) => {
            if (result == LEG_DELETE_CONFIRM_CLICK) {
                this.confirmDeleteLeg();
            }
            else {
                this.cancelDeleteLeg();
            }
        }, (reason) => { this.cancelDeleteLeg(); });
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
};
DriverDetailsComponent = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
        selector: 'app-driver-details',
        template: __webpack_require__(/*! ./driver-details.component.html */ "./src/app/driver-details/driver-details.component.html"),
        styles: [__webpack_require__(/*! ./driver-details.component.sass */ "./src/app/driver-details/driver-details.component.sass")]
    }),
    __metadata("design:paramtypes", [_driver_service__WEBPACK_IMPORTED_MODULE_3__["DriverService"],
        _leg_service__WEBPACK_IMPORTED_MODULE_4__["LegService"],
        _statistics_service__WEBPACK_IMPORTED_MODULE_5__["StatisticsService"],
        _auth_service__WEBPACK_IMPORTED_MODULE_6__["AuthService"],
        _node_modules_angular_router__WEBPACK_IMPORTED_MODULE_7__["ActivatedRoute"],
        _ng_bootstrap_ng_bootstrap__WEBPACK_IMPORTED_MODULE_1__["NgbModal"]])
], DriverDetailsComponent);



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
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm2015/operators/index.js");
/* harmony import */ var _leg_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./leg.service */ "./src/app/leg.service.ts");
/* harmony import */ var _auth_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./auth.service */ "./src/app/auth.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





const jsonHeader = { 'Content-Type': 'application/json' };
let DriverService = class DriverService {
    constructor(http, legService, authService) {
        this.http = http;
        this.legService = legService;
        this.authService = authService;
        this.driversUrl = '/api/driversapi';
    }
    getDrivers() {
        return this.authService.authHeader().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["map"])(ah => { return { headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"](ah) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["switchMap"])(h => this.http.get(this.driversUrl, h)));
    }
    getDriver(id) {
        const url = `${this.driversUrl}/${id}`;
        return this.authService.authHeader().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["map"])(ah => { return { headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"](ah) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["switchMap"])(h => this.http.get(url, h)));
    }
    updateDriver(driver) {
        const url = `${this.driversUrl}/${driver.driverID}`;
        return this.authService.authHeader().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["map"])(ah => { return { headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"](Object.assign(ah, jsonHeader)) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["switchMap"])(options => this.http.put(url, driver, options)));
    }
    addDriver(driver) {
        const url = `${this.driversUrl}/new`;
        return this.authService.authHeader().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["map"])(ah => { return { headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"](Object.assign(ah, jsonHeader)) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["switchMap"])(options => this.http.post(url, driver, options)));
    }
    deleteDriver(driver) {
        const id = typeof driver === 'number' ? driver : driver.driverID;
        const url = `${this.driversUrl}/${id}`;
        return this.authService.authHeader().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["map"])(ah => { return { headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"](Object.assign(ah, jsonHeader)) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["switchMap"])(options => this.http.delete(url, options)));
    }
};
DriverService = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
        providedIn: 'root'
    }),
    __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"],
        _leg_service__WEBPACK_IMPORTED_MODULE_3__["LegService"],
        _auth_service__WEBPACK_IMPORTED_MODULE_4__["AuthService"]])
], DriverService);



/***/ }),

/***/ "./src/app/drivers/drivers.component.html":
/*!************************************************!*\
  !*** ./src/app/drivers/drivers.component.html ***!
  \************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<ng-template [ngIf]=\"companyStatistics != null\">\r\n    <div class=\"card w-md-50 mb-3\">\r\n        <div class=\"card-body\">\r\n            <h5 class=\"card-title\">Statistics</h5>\r\n        </div>\r\n        <ul class=\"list-group list-group-flush\">\r\n            <li class=\"list-group-item\">{{companyStatistics.numOfDrivers}} driver<ng-template [ngIf]=\"companyStatistics.numOfDrivers != 1\">s</ng-template></li>\r\n            <li class=\"list-group-item\">{{companyStatistics.pickups}} passenger pickup<ng-template [ngIf]=\"companyStatistics.numOfPickups != 1\">s</ng-template></li>\r\n            <li class=\"list-group-item\">{{companyStatistics.milesDriven}} mile<ng-template [ngIf]=\"companyStatistics.milesDriven != 1\">s</ng-template> driven</li>\r\n            <li class=\"list-group-item\" *ngIf=\"companyStatistics.averagePickupDelay != null\">\r\n                Average pickup delay:\r\n                {{companyStatistics.averagePickupDelay}} minute<ng-template [ngIf]=\"companyStatistics.averagePickupDela != 1\">s</ng-template>\r\n            </li>\r\n            <li class=\"list-group-item\">Total fares: ${{companyStatistics.totalFares}}</li>\r\n            <li class=\"list-group-item\">Total fuel costs: ${{companyStatistics.totalCosts}}</li>\r\n            <li class=\"list-group-item\">Net profit: ${{companyStatistics.netProfit}}</li>\r\n        </ul>\r\n    </div>\r\n</ng-template>\r\n\r\n<div class=\"card w-100\">\r\n    <div class=\"card-body\">\r\n        <h5 class=\"card-title\">Drivers</h5>\r\n        <form (ngSubmit)=\"onSubmit()\" #driversForm=\"ngForm\">\r\n            <div class=\"table-responsive-md\">\r\n                <table class=\"table\">\r\n                    <thead>\r\n                        <tr>\r\n                            <th>\r\n                                Name\r\n                            </th>\r\n                            <th>\r\n                                License Number\r\n                            </th>\r\n                            <th></th>\r\n                        </tr>\r\n                    </thead>\r\n                    <tbody>\r\n                        <tr *ngFor=\"let driver of drivers\">\r\n                            <td [ngSwitch]=\"editing\">\r\n                                <input type=\"text\" class=\"form-control\" id=\"name\" required name=\"name_{{driver.driverID}}\" [(ngModel)]=\"driver.name\"\r\n                                       *ngSwitchCase=\"driver.driverID\" />\r\n                                <span *ngSwitchDefault>{{driver.name}}</span>\r\n                            </td>\r\n                            <td [ngSwitch]=\"editing\">\r\n                                <input type=\"text\" class=\"form-control\" id=\"licenseNumber\" required name=\"licenseNumber_{{driver.driverID}}\" [(ngModel)]=\"driver.licenseNumber\"\r\n                                       *ngSwitchCase=\"driver.driverID\" />\r\n                                <span *ngSwitchDefault>{{driver.licenseNumber}}</span>\r\n                            </td>\r\n                            <td [ngSwitch]=\"editing\">\r\n                                <div class=\"btn-toolbar\" role=\"toolbar\" *ngSwitchCase=\"driver.driverID\">\r\n                                    <button type=\"submit\" class=\"btn btn-primary mr-2\" [disabled]=\"!driversForm.form.valid\">Save</button>\r\n                                    <button type=\"button\" class=\"btn btn-secondary\" (click)=\"cancelEdit()\">Cancel</button>\r\n                                </div>\r\n                                <span class=\"btn-group\" role=\"group\" *ngSwitchDefault>\r\n                                    <a class=\"btn btn-secondary\" href=\"/Drivers/Details/{{driver.driverID}}\">Details</a>\r\n                                    <button type=\"button\" class=\"btn btn-secondary\" (click)=\"editClicked(driver)\">Edit</button>\r\n                                    <button type=\"button\" class=\"btn btn-secondary\" (click)=\"showDeleteConfirm(content, driver)\">Delete</button>\r\n                                </span>\r\n                            </td>\r\n                        </tr>\r\n                        <tr *ngIf=\"adding; else addLink\">\r\n                            <td>\r\n                                <input class=\"form-control\" required #name />\r\n                            </td>\r\n                            <td>\r\n                                <input class=\"form-control\" required #licenseNumber />\r\n                            </td>\r\n                            <td>\r\n                                <button type=\"submit\" class=\"btn btn-primary mr-2\" [disabled]=\"!driversForm.form.valid\" (click)=\"saveNew(name.value, licenseNumber.value); name.value=''; licenseNumber.value=''\">Save</button>\r\n                                <button type=\"button\" class=\"btn btn-secondary\" (click)=\"cancelAdd()\">Cancel</button>\r\n                            </td>\r\n                        </tr>\r\n                        <ng-template #addLink>\r\n                            <button type=\"button\" class=\"btn btn-primary\" (click)=\"addNew()\">New Driver</button>\r\n                        </ng-template>\r\n                    </tbody>\r\n                </table>\r\n            </div>\r\n        </form>\r\n     </div>\r\n</div>\r\n<!-- delete confirm modal -->\r\n<ng-template #content let-modal>\r\n    <div class=\"modal-header\">\r\n        <h5 class=\"modal-title\">Confirm Delete</h5>\r\n        <button type=\"button\" class=\"close\" aria-label=\"Close\" (click)=\"modal.dismiss('crossClicked')\">\r\n            <span aria-hidden=\"true\">&times;</span>\r\n        </button>\r\n    </div>\r\n    <div class=\"modal-body\">\r\n        <p>Are you sure you want to delete driver {{toDelete.name}}?</p>\r\n    </div>\r\n    <div class=\"modal-footer\">\r\n        <button type=\"button\" class=\"btn btn-primary\" (click)=\"modal.close('deleteConfirmed')\">Delete</button>\r\n        <button type=\"button\" class=\"btn btn-secondary\" (click)=\"modal.close('deleteCancelled')\">Cancel</button>\r\n    </div>\r\n</ng-template>"

/***/ }),

/***/ "./src/app/drivers/drivers.component.sass":
/*!************************************************!*\
  !*** ./src/app/drivers/drivers.component.sass ***!
  \************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2RyaXZlcnMvZHJpdmVycy5jb21wb25lbnQuc2FzcyJ9 */"

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
/* harmony import */ var _auth_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../auth.service */ "./src/app/auth.service.ts");
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
    constructor(driverService, statisticsService, authService, modalService) {
        this.driverService = driverService;
        this.statisticsService = statisticsService;
        this.authService = authService;
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
        this.authService.makeSessionUserToken();
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
        _auth_service__WEBPACK_IMPORTED_MODULE_4__["AuthService"],
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
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm2015/operators/index.js");
/* harmony import */ var _auth_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./auth.service */ "./src/app/auth.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




const jsonHeader = { 'Content-Type': 'application/json' };
let GeocodingService = class GeocodingService {
    constructor(http, authService) {
        this.http = http;
        this.authService = authService;
        this.geocodingUrl = "/api/geocoding";
    }
    getLegCoordinates(id) {
        const url = `${this.geocodingUrl}/${id}`;
        return this.authService.authHeader().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["map"])(ah => { return { headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"](Object.assign(ah, jsonHeader)) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["flatMap"])(options => this.http.get(url, options)));
    }
    getAddressCoordinates(address) {
        const url = `${this.geocodingUrl}/direct/${address}`;
        return this.authService.authHeader().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["map"])(ah => { return { headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"](Object.assign(ah, jsonHeader)) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["flatMap"])(options => this.http.get(url, options)));
    }
};
GeocodingService = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
        providedIn: 'root'
    }),
    __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"],
        _auth_service__WEBPACK_IMPORTED_MODULE_3__["AuthService"]])
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
/* harmony import */ var _auth_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./auth.service */ "./src/app/auth.service.ts");
/* harmony import */ var _node_modules_angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../node_modules/@angular/common/http */ "./node_modules/@angular/common/fesm2015/http.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm2015/operators/index.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




const jsonHeader = { 'Content-Type': 'application/json' };
let LegService = class LegService {
    constructor(http, authService) {
        this.http = http;
        this.authService = authService;
        this.legsUrl = '/api/legsapi';
    }
    getLegsAll() {
        return this.authService.authHeader()
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["map"])(ah => { return { headers: new _node_modules_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpHeaders"](ah) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["switchMap"])(h => this.http.get(this.legsUrl, h)));
    }
    getLegs(driverID) {
        const url = `${this.legsUrl}/fordriver/${driverID}`;
        return this.authService.authHeader()
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["map"])(ah => { return { headers: new _node_modules_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpHeaders"](ah) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["switchMap"])(h => { return this.http.get(url, h); }));
    }
    getLeg(id) {
        const url = `${this.legsUrl}/${id}`;
        return this.authService.authHeader()
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["map"])(ah => { return { headers: new _node_modules_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpHeaders"](ah) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["switchMap"])(h => this.http.get(url, h)));
    }
    createLeg(leg) {
        const url = `${this.legsUrl}/new`;
        return this.authService.authHeader()
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["map"])(ah => { return { headers: new _node_modules_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpHeaders"](Object.assign(ah, jsonHeader)) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["switchMap"])(h => this.http.post(url, leg, h)));
    }
    updateLeg(leg) {
        const url = `${this.legsUrl}/${leg.legID}`;
        return this.authService.authHeader()
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["map"])(ah => { return { headers: new _node_modules_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpHeaders"](Object.assign(ah, jsonHeader)) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["switchMap"])(h => this.http.put(url, leg, h)));
    }
    deleteLeg(leg) {
        const url = `${this.legsUrl}/${leg.legID}`;
        return this.authService.authHeader()
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["map"])(ah => { return { headers: new _node_modules_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpHeaders"](ah) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["switchMap"])(h => this.http.delete(url, h)));
    }
};
LegService = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
        providedIn: 'root'
    }),
    __metadata("design:paramtypes", [_node_modules_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"],
        _auth_service__WEBPACK_IMPORTED_MODULE_1__["AuthService"]])
], LegService);



/***/ }),

/***/ "./src/app/leg.ts":
/*!************************!*\
  !*** ./src/app/leg.ts ***!
  \************************/
/*! exports provided: Leg */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Leg", function() { return Leg; });
class Leg {
    getTotalFuelCost() {
        return this.fuelCost * this.distance;
    }
}


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

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2xlZ3MvbGVncy5jb21wb25lbnQuc2FzcyJ9 */"

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

/***/ "./src/app/login-model.ts":
/*!********************************!*\
  !*** ./src/app/login-model.ts ***!
  \********************************/
/*! exports provided: LoginModel, Input */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LoginModel", function() { return LoginModel; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Input", function() { return Input; });
class LoginModel {
    constructor(email, password) {
        this.input.email = email;
        this.input.password = password;
        this.input.rememberMe = false;
    }
}
class Input {
}


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
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm2015/operators/index.js");
/* harmony import */ var _auth_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./auth.service */ "./src/app/auth.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




const jsonHeader = { 'Content-Type': 'application/json' };
let PickupPredictorService = class PickupPredictorService {
    constructor(http, authService) {
        this.http = http;
        this.authService = authService;
        this.analysisUrl = '/api/analysisapi';
    }
    getFareClassIntervals() {
        const url = `${this.analysisUrl}/fareclassintervals`;
        return this.authService.authHeader().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["map"])(ah => { return { headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"](Object.assign(ah, jsonHeader)) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["flatMap"])(options => this.http.get(url, options)));
    }
    setFareClassIntervals(intervalBounds) {
        const url = `${this.analysisUrl}/fareclassintervals`;
        return this.authService.authHeader().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["map"])(ah => { return { headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"](Object.assign(ah, jsonHeader)) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["flatMap"])(options => this.http.put(url, intervalBounds, options)));
    }
    getFareClassProbabilities(startCoords, endCoords, delay, duration, pickups, interval) {
        const url = `${this.analysisUrl}/fareclassprob/${startCoords[0]}/${startCoords[1]}/${endCoords[0]}/${endCoords[1]}/${delay}/${duration}/${pickups}/${interval}`;
        return this.authService.authHeader().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["map"])(ah => { return { headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"](Object.assign(ah, jsonHeader)) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["flatMap"])(options => this.http.get(url, options)));
    }
    getPickupProbabilities(startCoords, endCoords, delay, duration, fare, interval) {
        const url = `${this.analysisUrl}/pickupprob/${startCoords[0]}/${startCoords[1]}/${endCoords[0]}/${endCoords[1]}/${delay}/${duration}/${fare}/${interval}`;
        return this.authService.authHeader().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["map"])(ah => { return { headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"](Object.assign(ah, jsonHeader)) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["flatMap"])(options => this.http.get(url, options)));
    }
};
PickupPredictorService = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
        providedIn: 'root'
    }),
    __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"],
        _auth_service__WEBPACK_IMPORTED_MODULE_3__["AuthService"]])
], PickupPredictorService);



/***/ }),

/***/ "./src/app/pickup-predictor/pickup-predictor.component.html":
/*!******************************************************************!*\
  !*** ./src/app/pickup-predictor/pickup-predictor.component.html ***!
  \******************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<h2>Pickup predictions</h2>\r\n<p>\r\n    Predict probability of completing given leg and collecting fares.\r\n</p>\r\n\r\n<form (ngSubmit)=\"onSubmit()\" #pickupPredictionForm=\"ngForm\">\r\n    <div class=\"row\">\r\n        <div class=\"col-md-6\">\r\n            <div class=\"card mb-2\">\r\n                <div class=\"card-header\">\r\n                    Leg information\r\n                </div>\r\n                <div class=\"card-body\">\r\n                    <div class=\"form-group\">\r\n                        <label class=\"card-text\" for=\"startAddress\">Start address</label>\r\n                        <input type=\"text\" name=\"startAddress\" required [(ngModel)]=\"startAddress\" class=\"form-control\" />\r\n                    </div>\r\n                    <div class=\"form-group\">\r\n                        <label class=\"card-text\" for=\"endAddress\">End address</label>\r\n                        <input type=\"text\" name=\"endAddress\" required [(ngModel)]=\"endAddress\" class=\"form-control\" />\r\n                    </div>\r\n                    <div class=\"form-group\">\r\n                        <label class=\"card-text\" for=\"pickupDelay\">Pickup delay (min)</label>\r\n                        <input type=\"number\" name=\"pickupDelay\" required [(ngModel)]=\"delay\" class=\"form-control\" />\r\n                    </div>\r\n                    <div class=\"form-group\">\r\n                        <label class=\"card-text\" for=\"duration\">Duration (min)</label>\r\n                        <input type=\"number\" name=\"duration\" required [(ngModel)]=\"duration\" class=\"form-control\" />\r\n                    </div>\r\n                    <div class=\"form-group\">\r\n                        <label class=\"card-text\" for=\"interval\">Interval over which to predict probabilities (min)</label>\r\n                        <input type=\"number\" name=\"interval\" required [(ngModel)]=\"interval\" class=\"form-control\" />\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"col-md-6\">\r\n            <div class=\"row\">\r\n                <div class=\"col\">\r\n                    <div class=\"card mb-2\">\r\n                        <div class=\"card-header\">\r\n                            Fare class probabilities\r\n                        </div>\r\n                        <div class=\"card-body\">\r\n                            <div class=\"form-group\">\r\n                                <label for=\"pickups\" class=\"card-text\">Number of pickups</label>\r\n                                <input type=\"number\" name=\"pickups\" [(ngModel)]=\"pickups\" class=\"form-control\" />\r\n                            </div>\r\n                            <button type=\"submit\" class=\"btn btn-primary\">Predict fare class probabilities</button>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <div class=\"row\">\r\n                <div class=\"col\">\r\n                    <div class=\"card mb-2\">\r\n                        <div class=\"card-header\">\r\n                            Pickup probabilities\r\n                        </div>\r\n                        <div class=\"card-body\">\r\n                            <div class=\"form-group\">\r\n                                <label for=\"fare\" class=\"card-text\">Fare</label>\r\n                                <input type=\"number\" name=\"fare\" [(ngModel)]=\"fare\" class=\"form-control\" />\r\n                            </div>\r\n                            <button type=\"button\" class=\"btn btn-primary\" (click)=\"predictPickupProbabilities()\">Predict pickup probabilities</button>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</form>\r\n\r\n<h2>Results</h2>\r\n<div class=\"row\">\r\n    <ng-template [ngIf]=\"fareClassProbabilities != null\">\r\n        <div class=\"col-md-6\">\r\n            <div class=\"card mb-2\">\r\n                <div class=\"card-header\">\r\n                    Fare class probabilities\r\n                </div>\r\n                <div class=\"card-body\">\r\n                    <div class=\"row\" *ngFor=\"let i of fareClassIndices\">\r\n                        <div class=\"col\">\r\n                            <p class=\"card-text pl-5\">${{i == 0 ? 0 : fareClassIntervalBoundaries[i-1]}} to ${{fareClassIntervalBoundaries[i]}}:</p>\r\n                        </div>\r\n                        <div class=\"col\">\r\n                            <p class=\"card-text pr-5\">{{fareClassProbabilities[i] * 100 | number:'1.1-1'}}%</p>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"row\" *ngIf=\"fareClassIntervalBoundaries.length > 0\">\r\n                        <div class=\"col\">\r\n                            <p class=\"card-text pl-5\">${{fareClassIntervalBoundaries[fareClassIntervalBoundaries.length - 1]}}+:</p>\r\n                        </div>\r\n                        <div class=\"col\">\r\n                            <p class=\"card-text pr-5\">{{fareClassProbabilities[fareClassIntervalBoundaries.length] * 100}}%</p>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </ng-template>\r\n    <ng-template [ngIf]=\"pickupProbabilities != null\">\r\n        <div class=\"col-md-6\">\r\n            <div class=\"card mb-2\">\r\n                <div class=\"card-header\">\r\n                    Pickup probabilities\r\n                </div>\r\n                <div class=\"card-body\">\r\n                    <div class=\"row\" *ngFor=\"let i of pickupProbabilityIndices\">\r\n                        <div class=\"col\">\r\n                            <p class=\"pl-5 card-text\" *ngIf=\"i == 0\">\r\n                                Probability of failure to collect:\r\n                            </p>\r\n                            <p class=\"pl-5 card-text\" *ngIf=\"i > 0\">\r\n                                Probability of collecting {{i}} passenger<ng-template [ngIf]=\"i != 1\">s</ng-template>:\r\n                            </p>\r\n                        </div>\r\n                        <div class=\"col\">\r\n                            <p class=\"pr-5 card-text\">{{pickupProbabilities[i] * 100 | number:'1.1-1'}}%</p>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </ng-template>\r\n</div>\r\n<div class=\"row\" id=\"pickupPredictionResults\">\r\n    <div class=\"col\"></div>\r\n</div>"

/***/ }),

/***/ "./src/app/pickup-predictor/pickup-predictor.component.scss":
/*!******************************************************************!*\
  !*** ./src/app/pickup-predictor/pickup-predictor.component.scss ***!
  \******************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3BpY2t1cC1wcmVkaWN0b3IvcGlja3VwLXByZWRpY3Rvci5jb21wb25lbnQuc2NzcyJ9 */"

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
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm2015/common.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm2015/router.js");
/* harmony import */ var _pickup_predictor_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../pickup-predictor.service */ "./src/app/pickup-predictor.service.ts");
/* harmony import */ var _geocoding_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../geocoding.service */ "./src/app/geocoding.service.ts");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm2015/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm2015/operators/index.js");
/* harmony import */ var _auth_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../auth.service */ "./src/app/auth.service.ts");
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
    constructor(pickupPredictorService, geocodingService, aRoute, viewportScroller, authService) {
        this.pickupPredictorService = pickupPredictorService;
        this.geocodingService = geocodingService;
        this.aRoute = aRoute;
        this.viewportScroller = viewportScroller;
        this.authService = authService;
    }
    geocodeInputAddresses(geoDependent) {
        const startCoords$ = this.geocodingService.getAddressCoordinates(this.startAddress);
        const endCoords$ = this.geocodingService.getAddressCoordinates(this.endAddress);
        return Object(rxjs__WEBPACK_IMPORTED_MODULE_5__["combineLatest"])(startCoords$, endCoords$, (startCoords, endCoords) => ({ startCoords, endCoords }))
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["switchMap"])(pair => {
            this.startCoords = pair.startCoords;
            this.endCoords = pair.endCoords;
            return geoDependent(this.startCoords, this.endCoords);
        }));
    }
    onSubmit() {
        this.geocodeInputAddresses((s, e) => this.pickupPredictorService.getFareClassProbabilities(s, e, this.delay, this.duration, this.pickups, this.interval))
            .subscribe(probs => {
            this.resultsJustComputed = true;
            this.fareClassProbabilities = probs;
        });
    }
    predictPickupProbabilities() {
        this.geocodeInputAddresses((s, e) => this.pickupPredictorService.getPickupProbabilities(s, e, this.delay, this.duration, this.fare, this.interval))
            .subscribe(probs => {
            this.resultsJustComputed = true;
            this.pickupProbabilities = probs;
            this.pickupProbabilityIndices = (new Array(probs.length)).fill(0).map((x, i) => i);
        });
    }
    ngOnInit() {
        this.authService.makeSessionUserToken();
        this.resultsJustComputed = false;
        this.pickupPredictorService.getFareClassIntervals()
            .subscribe(bounds => {
            this.fareClassIntervalBoundaries = bounds;
            this.fareClassIndices = (new Array(this.fareClassIntervalBoundaries.length))
                .fill(0).map((x, i) => i);
        });
    }
    ngAfterViewChecked() {
        if (this.resultsJustComputed) {
            this.scrollToResults();
            this.resultsJustComputed = false;
        }
    }
    scrollToResults() {
        this.viewportScroller.scrollToAnchor("pickupPredictionResults");
    }
};
PickupPredictorComponent = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
        selector: 'app-pickup-predictor',
        template: __webpack_require__(/*! ./pickup-predictor.component.html */ "./src/app/pickup-predictor/pickup-predictor.component.html"),
        styles: [__webpack_require__(/*! ./pickup-predictor.component.scss */ "./src/app/pickup-predictor/pickup-predictor.component.scss")]
    }),
    __metadata("design:paramtypes", [_pickup_predictor_service__WEBPACK_IMPORTED_MODULE_3__["PickupPredictorService"],
        _geocoding_service__WEBPACK_IMPORTED_MODULE_4__["GeocodingService"],
        _angular_router__WEBPACK_IMPORTED_MODULE_2__["ActivatedRoute"],
        _angular_common__WEBPACK_IMPORTED_MODULE_1__["ViewportScroller"],
        _auth_service__WEBPACK_IMPORTED_MODULE_7__["AuthService"]])
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
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm2015/operators/index.js");
/* harmony import */ var _auth_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./auth.service */ "./src/app/auth.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




const jsonHeader = { 'Content-Type': 'application/json' };
let PredictorService = class PredictorService {
    constructor(http, authService) {
        this.http = http;
        this.authService = authService;
        this.analysisUrl = '/api/analysisapi';
    }
    getRidershipProbabilities(driverID, delay, duration, fare) {
        const url = `${this.analysisUrl}/multipickupprob/${driverID}/${delay}/${duration}/${fare}`;
        return this.authService.authHeader().pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["map"])(ah => { return { headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"](Object.assign(ah, jsonHeader)) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["flatMap"])(options => this.http.get(url, options)));
    }
};
PredictorService = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
        providedIn: 'root'
    }),
    __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"],
        _auth_service__WEBPACK_IMPORTED_MODULE_3__["AuthService"]])
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

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3ByZWRpY3Rvci9wcmVkaWN0b3IuY29tcG9uZW50LnNhc3MifQ== */"

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
/* harmony import */ var _auth_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../auth.service */ "./src/app/auth.service.ts");
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
    constructor(predictorService, driverService, aRoute, authService) {
        this.predictorService = predictorService;
        this.driverService = driverService;
        this.aRoute = aRoute;
        this.authService = authService;
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
        this.authService.makeSessionUserToken();
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
        _angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"],
        _auth_service__WEBPACK_IMPORTED_MODULE_4__["AuthService"]])
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
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm2015/operators/index.js");
/* harmony import */ var _auth_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./auth.service */ "./src/app/auth.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




let StatisticsService = class StatisticsService {
    constructor(httpClient, authService) {
        this.httpClient = httpClient;
        this.authService = authService;
        this.analysisUrl = '/api/analysisapi';
    }
    getCompanyStatistics() {
        const url = `${this.analysisUrl}/company`;
        return this.authService.authHeader()
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["map"])(ah => { return { headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"](ah) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["flatMap"])(h => this.httpClient.get(url, h)));
    }
    getDriverStatistics(id) {
        const url = `${this.analysisUrl}/${id}`;
        return this.authService.authHeader()
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["map"])(ah => { return { headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"](ah) }; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["flatMap"])(h => this.httpClient.get(url, h)));
    }
};
StatisticsService = __decorate([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
        providedIn: 'root'
    }),
    __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"],
        _auth_service__WEBPACK_IMPORTED_MODULE_3__["AuthService"]])
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