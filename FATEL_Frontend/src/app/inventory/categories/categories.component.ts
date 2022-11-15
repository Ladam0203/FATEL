import {Component, OnInit} from '@angular/core';
import {Unit} from "../../entities/units";
import {Category} from "../../entities/category";
import {selectSearchbarQueryValue} from "../filter-bar.actions";
import {Store} from "@ngrx/store";
import {ItemService} from "../../services/item.service";
import {Item} from "../../entities/item";
import {selectShowAddItemComponentValue} from "../add-item.actions";

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
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
}
