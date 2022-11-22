import {Component, OnInit} from '@angular/core';
import {Entry} from "../entities/entry";
import {EntryService} from "../services/entry.service";

@Component({
  selector: 'diary',
  templateUrl: './diary.component.html',
  styleUrls: ['./diary.component.css']
})
export class DiaryComponent implements OnInit {

  displayedColumns: string[] = ['timestamp', 'itemName', 'change', 'quantityAfterChange'];

  entries: Entry[] = [];

  constructor(private entryService: EntryService) {
  }

  ngOnInit(): void {
    this.entryService.readAll()
      .then(entries => {
        this.entries = entries;
        console.log(entries);
      });
  }

}
