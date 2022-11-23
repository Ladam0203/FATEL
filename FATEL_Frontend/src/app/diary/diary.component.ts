import {Component, OnInit} from '@angular/core';
import {Entry} from "../entities/entry";
import {EntryService} from "../services/entry.service";
import {selectSearchbarQueryValue} from "../inventory/states/filter-bar.actions";
import {Store} from "@ngrx/store";

@Component({
  selector: 'diary',
  templateUrl: './diary.component.html',
  styleUrls: ['./diary.component.css']
})
export class DiaryComponent implements OnInit {

  displayedColumns: string[] = ['timestamp', 'itemName', 'change', 'quantityAfterChange'];

  entries: Entry[] = [];
  searchbarQuery = this.store.select(selectSearchbarQueryValue);

  constructor(private entryService: EntryService, private readonly store: Store<any>) {
  }

  ngOnInit(): void {
    this.entryService.readAll()
      .then(entries => {
        this.entries = entries;
      });
  }

}
