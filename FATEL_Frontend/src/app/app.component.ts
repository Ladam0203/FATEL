import {Component, OnInit} from '@angular/core';
import {Store} from "@ngrx/store";
import {Warehouse} from "./entities/warehouse";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'FATEL_Frontend';

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
