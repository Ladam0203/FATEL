import {Component, OnInit} from '@angular/core';
import {Store} from '@ngrx/store';
import {setShowAddItemComponent} from "../../states/app.states";
import {ReportService} from "../../services/report/report.service";
import {Unit} from "../../entities/units";
import {Warehouse} from "../../entities/warehouse";

@Component({
  selector: 'app-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.css']
})

export class ToolBarComponent implements OnInit {
  appState = this.store.select('appState');

  warehouse: Warehouse | undefined;
  units: typeof Unit = Unit;

  constructor(private readonly store: Store<any>, private readonly reportService: ReportService) {
  }

  ngOnInit(): void {
    this.appState.subscribe(state => {
      this.warehouse = state.selectedWarehouse;
    });
  }

  openAddItemComponent() {
    this.store.dispatch(setShowAddItemComponent());
  }

  exportPDF() {
    if (!this.warehouse) {
      return;
    }

    this.reportService.createReport(this.warehouse.name, ['Name', 'Width', 'Length', 'Unit', 'Quantity'] , this.warehouse.inventory);
  }
}
