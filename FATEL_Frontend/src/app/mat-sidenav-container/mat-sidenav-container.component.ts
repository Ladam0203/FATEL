import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {Store} from "@ngrx/store";
import {Warehouse} from "../entities/warehouse";
import {WarehouseService} from "../services/warehouse.service";
import {setSelectedWarehouse} from "../inventory/states/app.states";

@Component({
  selector: 'app-mat-sidenav-container',
  templateUrl: './mat-sidenav-container.component.html',
  styleUrls: ['./mat-sidenav-container.component.css']
})
export class MatSidenavContainerComponent implements OnInit {

  @Output() showInventoryEvent = new EventEmitter<boolean>();

  options = this._formBuilder.group({
    bottom: 0,
    fixed: true,
    top: 0,
  });

  appState = this.store.select('appState');

  warehouses: Warehouse[] = [];

  constructor(private _formBuilder: FormBuilder, private service: WarehouseService, private readonly store: Store<any>) {
  }

  ngOnInit(): void {
    this.service.readAll().then(warehouses => {
      this.warehouses = warehouses;

      let firstWarehouse = this.warehouses[0];
      if (firstWarehouse)
        this.store.dispatch(setSelectedWarehouse({warehouse: firstWarehouse}));
    });
  }

  showInventory() {
    this.showInventoryEvent.emit(true);
  }

  showDiary() {
    this.showInventoryEvent.emit(false);
  }

  onSelectWarehouse(warehouse: Warehouse) {
    this.store.dispatch(setSelectedWarehouse({warehouse: warehouse}));
  }
}
