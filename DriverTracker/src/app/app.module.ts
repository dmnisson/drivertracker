import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { DriversComponent } from './drivers/drivers.component';
import { LegsComponent } from './legs/legs.component';

@NgModule({
  declarations: [
    AppComponent,
    DriversComponent,
    LegsComponent
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
