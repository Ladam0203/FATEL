import {Component, OnInit} from '@angular/core';
import {Item} from "../entities/item";
import {ItemService} from "../services/item.service";
import {Category} from "../entities/category";
import {Unit} from "../entities/units";
import {Store} from "@ngrx/store";
import {selectShowAddItemComponentValue} from "./add-item.actions";
import {selectSearchbarQueryValue} from "./filter-bar.actions";

@Component({
  selector: 'inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  displayedColumns: string[] = ['name', 'width', 'length', 'unit', 'quantity', 'note'];

  units: typeof Unit = Unit;
  categories: Category[] = [];
  items: Item[] = [];

  searchbarQuery = this.store.select(selectSearchbarQueryValue);
  showAddItemComponent = this.store.select(selectShowAddItemComponentValue);

  constructor(private itemService: ItemService, private readonly store: Store<any>) {
  }

  ngOnInit(): void {
    this.itemService.readAll()
      .then(items => {
        this.items = items;
        this.categoriseItems();
      });
  }

  addItem(newItem: Item) {
    this.items.push(newItem);
    this.categoriseItems();
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
}
