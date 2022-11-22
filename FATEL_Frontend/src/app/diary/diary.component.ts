import { Component, OnInit } from '@angular/core';
import {Entry} from "../entities/entry";
import {EntryService} from "../services/entry.service";

@Component({
  selector: 'app-diary',
  templateUrl: './diary.component.html',
  styleUrls: ['./diary.component.css']
})
export class DiaryComponent implements OnInit {

  entries: Entry[] = [];

  constructor(private entryService: EntryService) { }

  ngOnInit(): void {
    this.entryService.readAll()
      .then(entries => this.entries = entries);
  }

}
