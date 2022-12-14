import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Item} from "../../entities/item";
import {Unit} from "../../entities/units";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ItemService} from "../../services/item.service";
import {Store} from "@ngrx/store";
import {greaterThanDirective} from "../../validators/greaterThan.directive";
import {PutItemDTO} from "../../entities/DTOs/PutItemDTO";
import {close} from "../../states/app.states";

@Component({
  selector: 'app-edit-item',
  templateUrl: './edit-item.component.html',
  styleUrls: ['./edit-item.component.css']
})
export class EditItemComponent implements OnInit {

  @Output() editItemEvent = new EventEmitter<any>();

  units: typeof Unit = Unit;

  itemForm: FormGroup = new FormGroup({
    name: new FormControl(),
    unit: new FormControl(),
    length: new FormControl(),
    width: new FormControl(),
    note: new FormControl(),
  });

  restrictedButtonUsage: boolean = false;

  text: string = 'EDIT-ITEM-COMPONENT.EDIT-BUTTON.LABEL.DEFAULT';
  confirmEdit: boolean = true;

  appState = this.store.select('appState');

  editingItem: Item | undefined;

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
        Validators.pattern(/^\d*(?:[.,]\d{1,3})?$/),
        greaterThanDirective(),
        Validators.required
      ]),
      width: new FormControl(null, [
        Validators.pattern(/^\d*(?:[.,]\d{1,3})?$/),
        greaterThanDirective(),
        Validators.required
      ]),
      note: new FormControl()
    });

    this.appState.subscribe(value => {
      this.editingItem = value.selectedItem;
      this.setFields(value.selectedItem);
    });
  }

  private setFields(item: Item) {
    if (!item)
      return;

    this.itemForm.setValue({
      name: item?.name,
      unit: item?.unit,
      length: item?.length,
      width: item?.width,
      note: item?.note,
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
    this.store.dispatch(close());
  }

  editItem() {
    if (this.itemForm.invalid) {
      this.restrictedButtonUsage = true;
      this.itemForm.markAllAsTouched();
      return;
    }

    if (this.confirmEdit) {
      this.text = 'EDIT-ITEM-COMPONENT.EDIT-BUTTON.LABEL.CONFIRM';
      this.confirmEdit = false;
      return;
    }

    if (!this.editingItem?.id)
      return;

    let updateItem: PutItemDTO = {
      id: this.editingItem?.id,
      name: this.itemForm.get('name')?.value,
      unit: this.itemForm.get('unit')?.value,
      length: this.itemForm.get('length')?.disabled ? null : this.itemForm.get('length')?.value,
      width: this.itemForm.get('width')?.disabled ? null : this.itemForm.get('width')?.value,
      note: this.itemForm.get('note')?.value == "" ? null : this.itemForm.get('note')?.value,
    }

    this.itemService.update(updateItem)
      .then(data => {
        this.editItemEvent.emit(data);
        this.closeEditItemComponent();
      });

    this.text = 'APPLY CHANGES';
    this.confirmEdit = true;
  }
}
