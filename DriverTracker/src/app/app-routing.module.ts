import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DriversComponent } from './drivers/drivers.component';
import { PredictorComponent } from './predictor/predictor.component';
import { PickupPredictorComponent } from './pickup-predictor/pickup-predictor.component';
import { DriverDetailsComponent } from './driver-details/driver-details.component';

const routes: Routes = [
    { path: 'Drivers', component: DriversComponent },
    { path: 'Predictor/Index/:id', component: PredictorComponent },
    { path: 'Drivers/Details/:id', component: DriverDetailsComponent },
    { path: 'PickupPredictor', component: PickupPredictorComponent }
];

@NgModule({
  imports: [
      RouterModule.forRoot(routes)
  ],
  exports: [
      RouterModule
  ]
})
export class AppRoutingModule { }
