import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {InventoryComponent} from './inventory/inventory.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HttpClientModule} from '@angular/common/http';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {NgMaterialModule} from './ng-material/ng-material.module';
import {MatTableModule} from '@angular/material/table';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatOptionModule} from '@angular/material/core';
import {KeysPipe} from './pipes/KeysPipe';
import {MatSelectModule} from '@angular/material/select';
import {AddItemComponent} from './inventory/add-item/add-item.component';
import {FilterPipe} from './pipes/FilterPipe';
import {MatSnackBar} from '@angular/material/snack-bar';
import {Overlay} from '@angular/cdk/overlay';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatSidenavContainerComponent} from './mat-sidenav-container/mat-sidenav-container.component';
import {ToolBarComponent} from './inventory/tool-bar/tool-bar.component';
import {StoreModule} from '@ngrx/store';
import {showAddItemComponentReducer} from "./inventory/states/add-item.actions";
import {FilterBarComponent} from './inventory/filter-bar/filter-bar.component';
import {searchbarQueryReducer} from "./inventory/states/filter-bar.actions";
import { CategoriesComponent } from './inventory/categories/categories.component';
import { EditItemComponent } from './inventory/edit-item/edit-item.component';
import {showEditItemComponentReducer} from "./inventory/states/edit-item.actions";

@NgModule({
  declarations: [
    AppComponent,
    InventoryComponent,
    KeysPipe,
    AddItemComponent,
    FilterPipe,
    MatSidenavContainerComponent,
    ToolBarComponent,
    FilterBarComponent,
    CategoriesComponent,
    EditItemComponent,
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
    StoreModule.forRoot({showAddItemComponent: showAddItemComponentReducer, searchbarQuery: searchbarQueryReducer, showEditItemComponent: showEditItemComponentReducer}),
  ],
  providers: [MatSnackBar, Overlay],
  bootstrap: [AppComponent],
})
export class AppModule {
}
