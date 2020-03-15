import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppNavbarComponent } from './app-navbar/app-navbar.component';
import { HomePageComponent } from './home-page/home-page.component';
import { AboutPageComponent } from './about-page/about-page.component';
import { DriversComponent } from './drivers/drivers.component';
import { PickupPredictorComponent } from './pickup-predictor/pickup-predictor.component';
import { HttpClientModule } from '@angular/common/http';
import { LoginPageComponent } from './login-page/login-page.component';
import { DriverDetailsComponent } from './driver-details/driver-details.component';

@NgModule({
  declarations: [
    AppComponent,
    AppNavbarComponent,
    HomePageComponent,
    AboutPageComponent,
    DriversComponent,
    DriverDetailsComponent,
    PickupPredictorComponent,
    LoginPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    NgbModule,
    FontAwesomeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
