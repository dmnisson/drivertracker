import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomePageComponent } from './home-page/home-page.component';
import { AboutPageComponent } from './about-page/about-page.component';
import { DriversComponent } from './drivers/drivers.component';
import { PickupPredictorComponent } from './pickup-predictor/pickup-predictor.component';
import { AuthGuardService } from './guards/auth-guard.service';
import { LoginPageComponent } from './login-page/login-page.component';

const routes: Routes = [
  {
    path: '',
    component: HomePageComponent
  },
  {
    path: 'login',
    component: LoginPageComponent
  },
  {
    path: 'about',
    component: AboutPageComponent
  },
  {
    path: 'statistics/drivers',
    component: DriversComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'predictions/pickup',
    component: PickupPredictorComponent,
    canActivate: [AuthGuardService]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuardService]
})
export class AppRoutingModule { }
