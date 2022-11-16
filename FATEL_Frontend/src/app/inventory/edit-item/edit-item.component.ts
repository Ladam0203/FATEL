import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Item} from "../../entities/item";
import {Unit} from "../../entities/units";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {selectShowAddItemComponentValue, setShowAddItemComponent} from "../states/add-item.actions";
import {ItemService} from "../../services/item.service";
import {Store} from "@ngrx/store";
import {greaterThanDirective} from "../../validators/greaterThan.directive";
import {PostItemDTO} from "../../entities/DTOs/PostItemDTO";
import {
  selectEditItemComponentItem,
  selectShowEditItemComponentValue,
  setShowEditItemComponent
} from "../states/edit-item.actions";

@Component({
  selector: 'app-edit-item',
  templateUrl: './edit-item.component.html',
  styleUrls: ['./edit-item.component.css']
})
export class EditItemComponent implements OnInit {

  @Output() editItemEvent = new EventEmitter<Item>();

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

  private showEditItemComponent = this.store.select(selectShowEditItemComponentValue);
  private editingItem = this.store.select(selectEditItemComponentItem);

  text: string = 'APPLY CHANGES';
  confirm: boolean = true;

  private setFields(itemToEdit: Item) {
    console.log(itemToEdit);
    this.itemForm.setValue({
      name: itemToEdit?.name,
      unit: itemToEdit?.unit,
      length: itemToEdit?.length,
      width: itemToEdit?.width,
      quantity: itemToEdit?.quantity,
      note: itemToEdit?.note,
    });
  }

  constructor(private itemService: ItemService, private readonly store: Store<any>) {
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
        greaterThanDirective(),
        Validators.required
      ]),
      width: new FormControl(null, [
        greaterThanDirective(),
        Validators.required
      ]),
      quantity: new FormControl(0, [
        Validators.min(0),
        Validators.required,
      ]),
      note: new FormControl()
    });

    this.editingItem.subscribe(item => {
      if (item)
        this.setFields(item);
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

  closeEditItemComponent() {
    this.itemForm.reset();
    this.store.dispatch(setShowEditItemComponent({value: false}));
  }

  editItem() {
    if (this.itemForm.invalid) {
      this.restrictedButtonUsage = true;
      this.itemForm.markAllAsTouched();
      return;
    }

    if (this.confirm) {
      this.text = 'CONFIRM';
      this.confirm = false;
      return;
    }

    /*let dto: PostItemDTO = {
      name: this.itemForm.get('name')?.value,
      length: this.itemForm.get('length')?.value,
      width: this.itemForm.get('width')?.value,
      unit: this.itemForm.get('unit')?.value,
      quantity: this.itemForm.get('quantity')?.value,
      note: this.itemForm.get('note')?.value
    }

    this.itemService.create(dto)
      .then(item => {
        this.editItemEvent.emit(item);
        this.closeEditItemComponent();
      });*/

    this.text = 'APPLY CHANGES';
    this.confirm = true;
  }
}
