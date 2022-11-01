import { Component, OnInit } from '@angular/core';
import {Entry} from "../entities/entry";
/*import {WarehouseService} from "../warehouse.service";*/
import {ENTRIES} from "../mock-objects/mock-entries";

@Component({
  selector: 'diary',
  templateUrl: './diary.component.html',
  styleUrls: ['./diary.component.css']
})
export class DiaryComponent implements OnInit {
  records = ENTRIES;

  constructor() { }

  ngOnInit(): void {
  }

}
