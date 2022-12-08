import {Component, OnInit} from '@angular/core';
import {Store} from "@ngrx/store";
import {Item} from "../entities/item";
import {selectSearchbarQueryValue} from "../states/filter-bar.actions";
import {Warehouse} from "../entities/warehouse";
import {Category} from "../entities/category";
import {Unit} from "../entities/units";

@Component({
  selector: 'app-mobile-inventory',
  templateUrl: './mobile-inventory.component.html',
  styleUrls: ['./mobile-inventory.component.css']
})
export class MobileInventoryComponent implements OnInit {
  // Table columns
  displayedColumns: string[] = ['name', 'width', 'length', 'unit', 'quantity', 'note', 'actions'];

  // Component variables
  items: Item[] = [];
  warehouse: Warehouse | undefined;
  categories: Category[] = [];
  units: typeof Unit = Unit;


  // State variables
  appState = this.store.select('appState');
  searchbarQuery = this.store.select(selectSearchbarQueryValue);

  constructor(private readonly store: Store<any>) {
  }

  ngOnInit(): void {
    this.appState.subscribe(state => {
      if (this.items != state.selectedWarehouse.inventory) {
        this.items = state.selectedWarehouse.inventory;
        this.categoriseItems();
      }

      this.warehouse = state.selectedWarehouse;
    });
    console.log(this.items);
  }

  onSelectWarehouse() {
    // go bak to selekting werhaus
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
