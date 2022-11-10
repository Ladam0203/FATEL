import { Component, OnInit } from '@angular/core';
import {Unit} from "../../entities/units";

@Component({
  selector: 'add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.css']
})
export class AddItemComponent implements OnInit {

  units: typeof Unit = Unit;
  quantity: number = 10;
  length: number = 10;
  width: number = 10;
  name: string = "";
  unit: Unit = Unit.Piece;

  constructor() { }

  ngOnInit(): void {
  }

}
