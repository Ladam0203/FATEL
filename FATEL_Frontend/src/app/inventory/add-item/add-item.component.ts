import {Component, OnInit} from '@angular/core';
import {Unit} from "../../entities/units";
import {Output, EventEmitter} from '@angular/core';
import {Item} from "../../entities/item";
import {ItemService} from "../../services/item.service";
import {PostItemDTO} from "../../entities/DTOs/PostItemDTO";
import {
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";

@Component({
  selector: 'add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.css']
})
export class AddItemComponent implements OnInit {

  @Output() newItemEvent = new EventEmitter<Item>();
  @Output() close: EventEmitter<void> = new EventEmitter<void>();

  units: typeof Unit = Unit;

  itemForm: FormGroup = new FormGroup({
    name: new FormControl(),
    unit: new FormControl(),
    length: new FormControl(),
    width: new FormControl(),
    quantity: new FormControl(),
    note: new FormControl(),
  });

  restrictedButtonUsage: boolean = false;

  constructor(private itemService: ItemService) {
  }

  ngOnInit(): void {

    this.itemForm = new FormGroup({
      name: new FormControl('', [
        Validators.required
      ]),
      unit: new FormControl(null, [
        Validators.required
      ]),
      length: new FormControl(null, [
        Validators.min(0),
        Validators.required
      ]),
      width: new FormControl(null, [
        Validators.min(0),
        Validators.required
      ]),
      quantity: new FormControl(0, [
        Validators.min(0),
        Validators.required,
      ]),
      note: new FormControl()
    });

  }

  shouldValidateLength() {
    if (this.itemForm.get('unit')?.value === Unit.Meter || this.itemForm.get('unit')?.value === Unit.SquareMeter) {
      this.itemForm.get('length')?.enable();
    } else {
      this.itemForm.get('length')?.disable();
    }
    return this.itemForm.get('length')?.enabled;
  }

  shouldValidateWidth() {
    if (this.itemForm.get('unit')?.value === Unit.SquareMeter) {
      this.itemForm.get('width')?.enable();
    } else {
      this.itemForm.get('width')?.disable();
    }
    return this.itemForm.get('width')?.enabled;
  }

  addNewItem() {
    if(this.itemForm.invalid){
      this.restrictedButtonUsage = true;
      this.itemForm.markAllAsTouched();
      return;
    }

    let dto: PostItemDTO = {
      name: this.itemForm.get('name')?.value,
      length: this.itemForm.get('length')?.value,
      width: this.itemForm.get('width')?.value,
      unit: this.itemForm.get('unit')?.value,
      quantity: this.itemForm.get('quantity')?.value,
      note: this.itemForm.get('note')?.value
    }

    this.itemService.create(dto)
      .then(item => {
        this.newItemEvent.emit(item);
        this.closeAddItemComponent();
      });
  }

  closeAddItemComponent() {
    this.itemForm.reset();
    this.close.emit();
  }
}
