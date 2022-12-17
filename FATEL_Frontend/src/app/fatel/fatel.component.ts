import {Component, OnInit} from '@angular/core';
import {Store} from "@ngrx/store";
import {Warehouse} from "../entities/warehouse";

@Component({
  selector: 'app-fatel',
  templateUrl: './fatel.component.html',
  styleUrls: ['./fatel.component.css']
})
export class FatelComponent implements OnInit {
  showInventory: boolean = true;

  selectedWarehouse: Warehouse | undefined;

  constructor(private readonly store: Store<any>) {}

  ngOnInit(): void {
    this.store.select('appState').subscribe(state => {
      this.selectedWarehouse = state.selectedWarehouse;
    });
  }

  onShowInventory(value: boolean) {
    this.showInventory = value;
  }

}
