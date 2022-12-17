import {Component, OnInit} from '@angular/core';
import {Store} from '@ngrx/store';
import {setShowAddItemComponent} from "../../states/app.states";
import {ReportService} from "../../services/report/report.service";
import {Warehouse} from "../../entities/warehouse";
import {selectSearchbarQueryValue} from "../../states/filter-bar.actions";

@Component({
  selector: 'app-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.css']
})

export class ToolBarComponent implements OnInit {
  appState = this.store.select('appState');

  warehouse: Warehouse | undefined;
  searchbarQuery = this.store.select(selectSearchbarQueryValue);
  query = '';

  constructor(private readonly store: Store<any>, private readonly reportService: ReportService) {
  }

  ngOnInit(): void {
    this.appState.subscribe(state => {
      this.warehouse = state.selectedWarehouse;
      this.searchbarQuery.subscribe(value => this.query = value);
    });
  }

  openAddItemComponent() {
    this.store.dispatch(setShowAddItemComponent());
  }

  exportPDF() {
    if (!this.warehouse) {
      return;
    }

    let filtered = this.warehouse.inventory
      .filter(item => item.name.toLowerCase().indexOf(this.query.toLowerCase() || '') !=-1);
    this.reportService.createInventoryReport(this.warehouse.name, filtered);
  }
}
