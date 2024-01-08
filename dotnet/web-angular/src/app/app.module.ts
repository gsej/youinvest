import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountSelectorComponent } from './account-selector/account-selector.component';
import { AccountSummaryComponent } from './account-summary/account-summary.component';
import { AccountContainerComponent } from './account-container/account-container.component';
import { AccountHistoryComponent } from './account-history/account-history.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountContainerComponent,
    AccountSelectorComponent,
    AccountSummaryComponent,
    AccountHistoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatCheckboxModule,
    MatListModule,
    MatTableModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
