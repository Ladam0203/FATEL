import {Component, OnInit, ViewChild} from '@angular/core';
import {Entry} from "../entities/entry";
import {EntryService} from "../services/entry.service";
import {selectSearchbarQueryValue} from "../states/filter-bar.actions";
import {Store} from "@ngrx/store";
import {Observable, of} from "rxjs";

@Component({
  selector: 'diary',
  templateUrl: './diary.component.html',
  styleUrls: ['./diary.component.css']
})
export class DiaryComponent implements OnInit {


  displayedColumns: string[] = ['timestamp', 'itemName', 'change', 'quantityAfterChange'];

  entries: Entry[] = [];
  sortedEntries: Entry[] = [];
  entryYears: string[] = [];

  searchbarQuery = this.store.select(selectSearchbarQueryValue);

  appState = this.store.select('appState');

  year: string = "";
  name: string = 'Warehouse';

  constructor(private entryService: EntryService, private readonly store: Store<any>) {
  }

  ngOnInit(): void {
    this.appState.subscribe(state => {
      this.name = state.selectedWarehouse.name;
      this.entries = state.selectedWarehouse.diary;
      this.getEntryYears();
      this.entries = [...this.entries].sort((a, b) => Number(new Date(a.timestamp)) - Number(new Date(b.timestamp))).reverse()
    })
  }

  getEntryYears(): void {
    for (const entry of this.entries) {
      let year = entry.timestamp.substring(0,4);
      if(!this.entryYears.includes(year))
        this.entryYears.push(year)
    }
    this.entryYears.push("2001");
  }
}
