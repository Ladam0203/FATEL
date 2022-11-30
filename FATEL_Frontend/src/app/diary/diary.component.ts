import {Component, OnInit, ViewChild} from '@angular/core';
import {Entry} from "../entities/entry";
import {EntryService} from "../services/entry.service";
import {selectSearchbarQueryValue} from "../states/filter-bar.actions";
import {Store} from "@ngrx/store";
import {Warehouse} from "../entities/warehouse";
import {ReportService} from "../services/report/report.service";

@Component({
  selector: 'diary',
  templateUrl: './diary.component.html',
  styleUrls: ['./diary.component.css']
})
export class DiaryComponent implements OnInit {


  displayedColumns: string[] = ['timestamp', 'itemName', 'change', 'quantityAfterChange'];

  entries: Entry[] = [];
  sortedEntries: Entry[] = [];
  searchbarQuery = this.store.select(selectSearchbarQueryValue);

  appState = this.store.select('appState');

  name: string = 'Warehouse';

  warehouse: Warehouse | undefined;
  searchBarQuery: String | undefined;

  constructor(private entryService: EntryService, private readonly store: Store<any>, private readonly reportService: ReportService) {
  }

  ngOnInit(): void {
    this.appState.subscribe(state => {
      this.name = state.selectedWarehouse.name;
      this.entries = state.selectedWarehouse.diary;
      this.entries = [...this.entries].sort((a, b) => Number(new Date(a.timestamp)) - Number(new Date(b.timestamp))).reverse()
      this.warehouse = state.selectedWarehouse;
    })
  }

  exportPDF() {
    if (!this.warehouse) {
      return;
    }
    let filtered = this.warehouse.diary.filter(entry =>
      entry.itemName.toLowerCase().indexOf(this.searchBarQuery?.toLowerCase() || '') != -1);
    console.log(filtered);
    this.reportService.createDiaryReport(this.warehouse.name, ['Timestamp', 'Item Name', 'Change', 'After'], this.warehouse.diary)
  }
}
