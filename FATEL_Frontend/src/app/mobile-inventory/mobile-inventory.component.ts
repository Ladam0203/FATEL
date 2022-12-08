import { Component, OnInit } from '@angular/core';
import {Store} from "@ngrx/store";
import {Item} from "../entities/item";
import {selectSearchbarQueryValue} from "../states/filter-bar.actions";
import {Warehouse} from "../entities/warehouse";
import {Router} from "@angular/router";

@Component({
  selector: 'app-mobile-inventory',
  templateUrl: './mobile-inventory.component.html',
  styleUrls: ['./mobile-inventory.component.css']
})
export class MobileInventoryComponent implements OnInit {
  //Component variables
  items: Item[] = [];
  warehouse: Warehouse | undefined;

  //State variables
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
      }

      this.warehouse = state.selectedWarehouse;
    });
  }

  onSelectWarehouse() {
    this.router.navigate(['/mobile']);
  }
}
