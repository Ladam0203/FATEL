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
import {FilterBarComponent} from './inventory/filter-bar/filter-bar.component';
import {searchbarQueryReducer} from "./states/filter-bar.actions";
import {CategoriesComponent} from './inventory/categories/categories.component';
import {EditItemComponent} from './inventory/edit-item/edit-item.component';
import {AppReducer} from "./states/app.states";
import {RecordMovementComponent} from './inventory/record-movement/record-movement.component';
import {DiaryComponent} from './diary/diary.component';
import {MatSortModule} from "@angular/material/sort";
import { WarehouseActionBarComponent } from './warehouse-action-bar/warehouse-action-bar.component';
import { NoWarehouseComponent } from './no-warehouse/no-warehouse.component';
import { LoginComponent } from './login/login.component';
import { FatelComponent } from './fatel/fatel.component';
import {RouterOutlet} from "@angular/router";
import {AppRoutingModule} from "./app-routing.module";

// import ngx-translate and the http loader
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import {HttpClient} from '@angular/common/http';
import { EditCategoryComponent } from './inventory/edit-category/edit-category.component';
import { MobileComponent } from './mobile/mobile.component';
import { MobileLoginComponent } from './mobile-login/mobile-login.component';
import { MobileNavComponent } from './mobile-nav/mobile-nav.component';

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
    RecordMovementComponent,
    DiaryComponent,
    WarehouseActionBarComponent,
    NoWarehouseComponent,
    LoginComponent,
    FatelComponent,
    EditCategoryComponent,
    MobileComponent,
    MobileLoginComponent,
    MobileNavComponent,
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
    StoreModule.forRoot({searchbarQuery: searchbarQueryReducer, appState: AppReducer}),
    MatSortModule,
    RouterOutlet,
    AppRoutingModule,
    TranslateModule.forRoot({
          loader: {
            provide: TranslateLoader,
            useFactory: HttpLoaderFactory,
            deps: [HttpClient]
          }
        })
  ],

  providers: [MatSnackBar, Overlay],
  bootstrap: [AppComponent],
})
export class AppModule {
}

export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http);
}
