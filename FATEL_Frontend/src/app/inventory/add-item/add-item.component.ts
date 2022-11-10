import { Component, OnInit } from '@angular/core';
import {Unit} from "../../entities/units";
import { Output, EventEmitter } from '@angular/core';
import {Item} from "../../entities/item";
import {ItemService} from "../../services/item.service";
import {PostItemDTO} from "../../entities/DTOs/PostItemDTO";

@Component({
  selector: 'add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.css']
})
export class AddItemComponent implements OnInit {

  @Output() newItemEvent = new EventEmitter<Item>();

  name: string = "";
  length?: number;
  width?: number;
  unit: Unit = Unit.Piece;
  quantity: number = 10;
  note?: string;
  units: typeof Unit = Unit;

  constructor(private itemService: ItemService) { }

  ngOnInit(): void {
  }

  addNewItem() {
    let dto: PostItemDTO = {
      name: this.name,
      length: this.length??null,
      width: this.width??null,
      unit: this.unit,
      quantity: this.quantity,
      note: this.note??null
    }
    this.itemService.create(dto)
      .then(item => this.newItemEvent.emit(item));

  }

}
