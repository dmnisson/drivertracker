import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DriversComponent } from './drivers/drivers.component';
import { PredictorComponent } from './predictor/predictor.component';
import { PickupPredictorComponent } from './pickup-predictor/pickup-predictor.component';

const routes: Routes = [
    { path: 'Drivers', component: DriversComponent },
    { path: 'Predictor/Index/:id', component: PredictorComponent },
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
