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
  setShowRecordMovementComponent
} from "../states/categories.states";

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

  categoriesState = this.store.select('categoriesState');

  showAddItem: boolean = false;
  showEditItem: boolean = false;
  showRecordMovement: boolean = false;
  closed: boolean = true;

  confirmDelete: boolean = true;
  deletingId: number | undefined;

  constructor(private itemService: ItemService, private readonly store: Store<any>) {
  }

  ngOnInit(): void {
    this.itemService.readAll()
      .then(items => {
        this.items = items;
        this.categoriseItems();
      });

    this.categoriesState.subscribe(value => {
      this.showAddItem = value.showAddItem;
      this.showEditItem = value.showEditItem;
      this.showRecordMovement = value.showRecordMovement;
      this.closed = value.closed;

      this.selectedItem = value.selectedItem;
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
    this.items.push(newItem);
    this.categoriseItems();
  }

  editItem(editItem: Item) {
    this.items = this.items.filter(item => item.id != editItem.id);
    this.items.push(editItem);
    this.categoriseItems();
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
