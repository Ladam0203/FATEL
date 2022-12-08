import { Component, OnInit } from '@angular/core';
import {Store} from "@ngrx/store";
import {Item} from "../entities/item";
import {selectSearchbarQueryValue} from "../states/filter-bar.actions";
import {Warehouse} from "../entities/warehouse";

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

  constructor(private readonly store: Store<any>) { }

  ngOnInit(): void {
    this.appState.subscribe(state => {
      if (this.items != state.selectedWarehouse.inventory) {
        this.items = state.selectedWarehouse.inventory;
      }

      this.warehouse = state.selectedWarehouse;
    });
    console.log(this.items);
  }

}
