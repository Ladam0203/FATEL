import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {greaterThanDirective} from "../../validators/greaterThan.directive";
import {ItemService} from "../../services/item.service";
import {Store} from "@ngrx/store";
import {Item} from "../../entities/item";

@Component({
  selector: 'app-record-movement',
  templateUrl: './record-movement.component.html',
  styleUrls: ['./record-movement.component.css']
})
export class RecordMovementComponent implements OnInit {

  constructor(private itemService: ItemService, private readonly store: Store<any>) {
  }

  movementForm: FormGroup = new FormGroup({
    change: new FormControl(),
  });

  categoriesState = this.store.select('categoriesState');

  itemToRecordMovementOn: Item | undefined;

  ngOnInit(): void {
    this.movementForm = new FormGroup({
      change: new FormControl('', [
        Validators.required,
        greaterThanDirective()
     ])
    });
  }
}
