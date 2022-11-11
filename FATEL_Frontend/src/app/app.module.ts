import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { WarehouseComponent } from './warehouse/warehouse.component';
import { InventoryComponent } from './inventory/inventory.component';
import { DiaryComponent } from './diary/diary.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {HttpClientModule} from "@angular/common/http";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {NgMaterialModule} from "./ng-material/ng-material.module";
import {MatTableModule} from "@angular/material/table";
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import {MatOptionModule} from "@angular/material/core";
import {KeysPipe} from "./pipes/KeysPipe";
import {MatSelectModule} from "@angular/material/select";
import { AddItemComponent } from './inventory/add-item/add-item.component';
import {FilterPipe} from "./pipes/FilterPipe";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Overlay} from "@angular/cdk/overlay";
import {MatAutocompleteModule} from "@angular/material/autocomplete";
import {RxwebValidators} from "@rxweb/reactive-form-validators";

@NgModule({
  declarations: [
    AppComponent,
    WarehouseComponent,
    InventoryComponent,
    DiaryComponent,
    KeysPipe,
    AddItemComponent,
    FilterPipe
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    NgbModule,
    NgMaterialModule,
    MatTableModule,
    MatSidenavModule,
    MatCheckboxModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatOptionModule,
    MatSelectModule,
    MatAutocompleteModule,
  ],
  providers: [
    MatSnackBar,
    Overlay,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
