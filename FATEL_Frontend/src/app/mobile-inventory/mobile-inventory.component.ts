import {Component, OnInit} from '@angular/core';
import {Store} from "@ngrx/store";
import {Item} from "../entities/item";
import {selectSearchbarQueryValue} from "../states/filter-bar.actions";
import {Warehouse} from "../entities/warehouse";
import {Category} from "../entities/category";
import {Unit} from "../entities/units";
import { Router } from '@angular/router';

@Component({
  selector: 'app-mobile-inventory',
  templateUrl: './mobile-inventory.component.html',
  styleUrls: ['./mobile-inventory.component.css']
})
export class MobileInventoryComponent implements OnInit {
  // Table columns
  displayedColumns: string[] = ['width', 'length', 'quantity'];

  // Component variables
  items: Item[] = [];
  warehouse: Warehouse | undefined;
  categories: Category[] = [];
  units: typeof Unit = Unit;


  // State variables
  appState = this.store.select('appState');
  searchbarQuery = this.store.select(selectSearchbarQueryValue);

  constructor(private readonly store: Store<any>, private readonly router : Router) { }

  ngOnInit(): void {
    this.appState.subscribe(state => {
      if (state.selectedWarehouse.id == 0) {
        this.onSelectWarehouse();
      }

      if (this.items != state.selectedWarehouse.inventory) {
        this.items = state.selectedWarehouse.inventory;
        this.categoriseItems();
      }

      this.warehouse = state.selectedWarehouse;
    });
  }

  onSelectWarehouse() {
    this.router.navigate(['/mobile']);
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
}
