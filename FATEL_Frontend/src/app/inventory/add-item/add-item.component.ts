import { Component, OnInit } from '@angular/core';
import {Unit} from "../../entities/units";
import { Output, EventEmitter } from '@angular/core';
import {Item} from "../../entities/item";
import {ItemService} from "../../services/item.service";
import {PostItemDTO} from "../../entities/DTOs/PostItemDTO";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {requiredIfMatches} from "../../validators/requiredIfMatches";
import {RxwebValidators} from "@rxweb/reactive-form-validators";

@Component({
  selector: 'add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.css']
})
export class AddItemComponent implements OnInit {

  @Output() newItemEvent = new EventEmitter<Item>();

  units: typeof Unit = Unit;

  itemForm: FormGroup = new FormGroup({
    name: new FormControl(),
    unit: new FormControl(),
    length: new FormControl(),
    width: new FormControl(),
    quantity: new FormControl(),
    note: new FormControl(),
  });

  constructor(private itemService: ItemService) { }

  ngOnInit(): void {

    this.itemForm = new FormGroup({
      name: new FormControl('',[
        Validators.required
      ]),
      unit: new FormControl(Unit.Piece,[
        Validators.required
      ]),
     length: new FormControl(null,[
        Validators.min(0),
       RxwebValidators.required({conditionalExpression:'x => x.unit.value == Unit.Meter' }),
      ]),
      width: new FormControl(null,[
        Validators.min(0),
        RxwebValidators.required({conditionalExpression:'x => x.unit.value == Unit.Meter' }),
      ]),
      quantity: new FormControl(0,[
        Validators.required,
        Validators.min(0),
      ]),
      notes: new FormControl()
    })
  }

  addNewItem() {
    let dto: PostItemDTO = {
      name:this.itemForm.get('name')?.value,
      length: this.itemForm.get('length')?.value,
      width: this.itemForm.get('width')?.value,
      unit: this.itemForm.get('unit')?.value,
      quantity: this.itemForm.get('quantity')?.value,
      note: this.itemForm.get('note')?.value
    }
    this.itemService.create(dto)
      .then(item =>{
        console.log(dto);
        this.newItemEvent.emit(item);
        this.itemForm.reset();
      })
  }

}
