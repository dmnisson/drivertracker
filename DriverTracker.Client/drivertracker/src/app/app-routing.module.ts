import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomePageComponent } from './home-page/home-page.component';
import { AboutPageComponent } from './about-page/about-page.component';
import { DriversComponent } from './drivers/drivers.component';
import { PickupPredictorComponent } from './pickup-predictor/pickup-predictor.component';

const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'about', component: AboutPageComponent },
  { path: 'statistics/drivers', component: DriversComponent },
  { path: 'predictions/pickup', component: PickupPredictorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
