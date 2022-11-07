import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatGridListModule } from '@angular/material/grid-list'
import { MatNativeDateModule } from '@angular/material/core';
import { CdkAccordionModule } from '@angular/cdk/accordion';
import { ListComponent } from './list/list.component';
import { NextPanelComponent } from './next-panel/next-panel.component';

@NgModule({
  declarations: [
    ListComponent,
    NextPanelComponent
  ],
  imports: [
    CommonModule,
    MatExpansionModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatGridListModule,
    CdkAccordionModule
  ],
  exports: [
    MatExpansionModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatGridListModule,
    CdkAccordionModule,

    ListComponent,
    NextPanelComponent
  ]
})
export class NgMaterialModule { }
