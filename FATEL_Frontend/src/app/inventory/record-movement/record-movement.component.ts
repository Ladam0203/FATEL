import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {greaterThanDirective} from "../../validators/greaterThan.directive";
import {ItemService} from "../../services/item.service";
import {Store} from "@ngrx/store";
import {Item} from "../../entities/item";
import {Movement} from "../../entities/DTOs/Movement";

@Component({
  selector: 'app-record-movement',
  templateUrl: './record-movement.component.html',
  styleUrls: ['./record-movement.component.css']
})
export class RecordMovementComponent implements OnInit {

  movementForm: FormGroup = new FormGroup({
    change: new FormControl(),
  });

  categoriesState = this.store.select('categoriesState');

  itemToRecordMovementOn: Item | undefined;

  restrictedButtonUsage: boolean = false;
  confirmMove: boolean = true;

  constructor(private itemService: ItemService, private readonly store: Store<any>) {
  }

  ngOnInit(): void {
    this.movementForm = new FormGroup({
      change: new FormControl('', [
        Validators.required,
        greaterThanDirective()
      ])
    });

    this.categoriesState.subscribe(value => {
        this.itemToRecordMovementOn = value.itemToRecordMovementOn;
      });
  }

  deposit() {
    /*
    if(!this.validation())
      return;
    */
    let movement: Movement = {
      // @ts-ignore
      item: this.itemToRecordMovementOn,
      change: this.movementForm.get('change')?.value,
    }

    this.itemService.updateQuantity(movement).then(value => console.log(value));
  }

  withdraw() {
    /*
    if (!this.validation())
      return;
    */
    let movement: Movement = {
      // @ts-ignore
      item: this.itemToRecordMovementOn,
      change: -(this.movementForm.get('change')?.value),
    }

    this.itemService.updateQuantity(movement).then(value => console.log(value));
  }

  validation(): boolean {
    if (this.movementForm.invalid) {
      this.restrictedButtonUsage = true;
      this.movementForm.markAllAsTouched();
      return false;
    }

    if (this.confirmMove) {
      this.confirmMove = false;
      return false;
    }

    if (!this.itemToRecordMovementOn?.id)
      return false;

    return true;
  }
}
