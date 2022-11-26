import {Component, OnInit} from '@angular/core';
import {Store} from '@ngrx/store';
import { Warehouse } from 'src/app/entities/warehouse';
import {deleteWarehouseAction, editWarehouseAction, setShowAddItemComponent} from "../states/app.states";
import {WarehouseService} from "../../services/warehouse.service";

@Component({
  selector: 'app-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.css']
})
export class ToolBarComponent implements OnInit {

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

  openAddItemComponent() {
    this.store.dispatch(setShowAddItemComponent());
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
