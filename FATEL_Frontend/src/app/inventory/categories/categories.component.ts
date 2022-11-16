import {Component, OnInit} from '@angular/core';
import {Unit} from "../../entities/units";
import {Category} from "../../entities/category";
import {selectSearchbarQueryValue} from "../states/filter-bar.actions";
import {Store} from "@ngrx/store";
import {ItemService} from "../../services/item.service";
import {Item} from "../../entities/item";
import {
  setShowEditItemComponent
} from "../states/categories.states";

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  displayedColumns: string[] = ['name', 'width', 'length', 'unit', 'quantity', 'note', 'edit'];

  units: typeof Unit = Unit;
  categories: Category[] = [];
  items: Item[] = [];

  searchbarQuery = this.store.select(selectSearchbarQueryValue);

  editingId: number | undefined;

  categoriesState = this.store.select('categoriesState');

  showAddItem: boolean = false;
  showEditItem: boolean = false;
  closed: boolean = true;

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
      this.closed = value.closed;
      this.editingId = value.editingItem?.id;
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
    this.store.dispatch(setShowEditItemComponent({item: itemToEdit}));
  }
}
