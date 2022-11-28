import { Component, OnInit } from '@angular/core';
import {Warehouse} from "../entities/warehouse";
import {Store} from "@ngrx/store";
import {WarehouseService} from "../services/warehouse.service";
import {deleteWarehouseAction, editWarehouseAction} from "../states/app.states";

@Component({
  selector: 'app-warehouse-action-bar',
  templateUrl: './warehouse-action-bar.component.html',
  styleUrls: ['./warehouse-action-bar.component.css']
})
export class WarehouseActionBarComponent implements OnInit {

  appState = this.store.select('appState');

  name : string | undefined;
  warehouse : Warehouse | undefined;

  editing: boolean = false;
  deleting: boolean = false;

  constructor(private readonly store: Store<any>, private service: WarehouseService) { }

  ngOnInit(): void {
    this.appState.subscribe(state => {
      this.warehouse = state.selectedWarehouse;
      this.name = this.warehouse?.name;
    });
  }

  onEditWarehouse() {
    this.editing = true;
  }

  editWarehouse() {
    if (!this.warehouse) {
      return;
    }
    if (!this.name) {
      return;
    }
    if (this.name == '') {
      return;
    }

    this.editing = false;

    this.service.update({id: this.warehouse.id, name: this.name})
      .then(warehouse =>
      {
        this.store.dispatch(editWarehouseAction({warehouse: warehouse}));
      });
  }

  deleteWarehouse() {

    if(!this.deleting){
      this.deleting = true;
      setTimeout(() => {
        this.deleting = false;
      }, 3000);
      return;
    }

    if (!this.warehouse) {
      return;
    }

    this.service.delete(this.warehouse.id)
      .then(warehouse => {
        this.store.dispatch(deleteWarehouseAction({warehouse: warehouse}));
      })

    this.deleting = false;
  }
}
