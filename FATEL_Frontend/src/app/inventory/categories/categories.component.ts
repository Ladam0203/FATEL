import {Component, OnInit} from '@angular/core';
import {Unit} from "../../entities/units";
import {Category} from "../../entities/category";
import {selectSearchbarQueryValue} from "../states/filter-bar.actions";
import {Store} from "@ngrx/store";
import {ItemService} from "../../services/item.service";
import {Item} from "../../entities/item";
import {
  close,
  setShowEditItemComponent,
  setShowRecordMovementComponent,
  addItemAction,
  editItemAction,
} from "../states/app.states";
import {Warehouse} from "../../entities/warehouse";

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  displayedColumns: string[] = ['name', 'width', 'length', 'unit', 'quantity', 'note', 'actions'];

  units: typeof Unit = Unit;
  categories: Category[] = [];
  items: Item[] = [];

  searchbarQuery = this.store.select(selectSearchbarQueryValue);

  selectedItem: Item | undefined;

  appState = this.store.select('appState');

  showAddItem: boolean = false;
  showEditItem: boolean = false;
  showRecordMovement: boolean = false;

  closed: boolean = true;

  warehouse: Warehouse | undefined;

  confirmDelete: boolean = true;
  deletingId: number | undefined;

  constructor(private itemService: ItemService, private readonly store: Store<any>) {
  }

  ngOnInit(): void {
    this.appState.subscribe(state => {
      this.showAddItem = state.showAddItem;
      this.showEditItem = state.showEditItem;
      this.showRecordMovement = state.showRecordMovement;
      this.closed = state.closed;

      this.selectedItem = state.selectedItem;

      if (this.items != state.selectedWarehouse.inventory) {
        this.items = state.selectedWarehouse.inventory;
        this.categoriseItems();
      }

      this.warehouse = state.selectedWarehouse;
    });
  }

  private categoriseItems(): void {
    this.categories = [];
    for (const item of this.items) {
      let category = this.categories.find(c => c.name === item.name && c.unit === item.unit);
      if (category == null) {
        category = new Category(item.name, item.unit);
        this.categories.push(category);
      }
      category.items.push(item);
    }
    this.categories.sort((a, b) => a.name.localeCompare(b.name));
  }

  addItem(newItem: Item) {
    this.store.dispatch(addItemAction({item: newItem}));
  }

  editItem(editItem: Item) {
    this.store.dispatch(editItemAction({item: editItem}));
  }

  openEditItemComponent(itemToEdit: Item) {
    this.confirmDelete = true;
    this.deletingId = undefined;
    this.store.dispatch(setShowEditItemComponent({item: itemToEdit}));
  }

  deleteItem(itemToDelete: Item) {

    setTimeout(() => {
      this.deletingId = undefined;
      this.confirmDelete = true;
    }, 1500);

    if (this.confirmDelete) {
      this.deletingId = itemToDelete.id;
      this.confirmDelete = false;
      return;
    }

    if (this.deletingId != itemToDelete.id) {
      this.deletingId = itemToDelete.id;
      return;
    }

    this.itemService.delete(itemToDelete.id)
      .then(item => {
        this.items = this.items.filter(items => items.id != item.id);
        this.categoriseItems();
      })

    this.deletingId = undefined;
    this.confirmDelete = true;
    this.store.dispatch(close());
  }

  openRecordMovementComponent(itemToRecordMovementOn: Item) {
    this.store.dispatch(setShowRecordMovementComponent({item: itemToRecordMovementOn}));
  }
}
