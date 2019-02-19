import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { DriversComponent } from './drivers/drivers.component';
import { LegsComponent } from './legs/legs.component';
import { AppRoutingModule } from './app-routing.module';
import { PredictorComponent } from './predictor/predictor.component';
import { PickupPredictorComponent } from './pickup-predictor/pickup-predictor.component';

@NgModule({
  declarations: [
    AppComponent,
    DriversComponent,
    LegsComponent,
    PredictorComponent,
    PickupPredictorComponent
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      AppRoutingModule,
      NgbModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
