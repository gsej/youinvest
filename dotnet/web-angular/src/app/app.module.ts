import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatListModule } from '@angular/material/list';



import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountSelectorComponent } from './account-selector/account-selector.component';
import { AccountSummaryComponent } from './account-summary/account-summary.component';
import { AccountContainerComponent } from './account-container/account-container.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountContainerComponent,
    AccountSelectorComponent,
    AccountSummaryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatCheckboxModule,
    MatListModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
