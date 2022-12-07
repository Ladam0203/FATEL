import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {Store} from "@ngrx/store";
import {Warehouse} from "../entities/warehouse";
import {WarehouseService} from "../services/warehouse.service";
import {setSelectedWarehouse} from "../states/app.states";
import {Router} from "@angular/router";

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

  inventoryActive: boolean = true;
  warehouseActive: number = -1;

  warehouses: Warehouse[] = [];

  constructor(private _formBuilder: FormBuilder, private service: WarehouseService, private readonly store: Store<any>, private router: Router) {
  }

  ngOnInit(): void {
    this.service.readAll().then(warehouses => {
      this.warehouses = warehouses;

      let firstWarehouse = this.warehouses[0];
      if (!firstWarehouse) {
        this.warehouseActive = -1;
      }
      this.store.dispatch(setSelectedWarehouse({warehouse: firstWarehouse}));
    });

    this.appState.subscribe(state => {
      //If there is no warehouse, does not select either
      if (!state.selectedWarehouse) {
        this.warehouseActive = -1;
        return;
      }
      //If a warehouse is deleted (it's name is set to null), select the one before and make the deleted one vanish
      if (state.selectedWarehouse.name == null) {
        let index = this.warehouses.findIndex(warehouse => warehouse.id == state.selectedWarehouse.id) - 1;
        this.store.dispatch(setSelectedWarehouse({warehouse: this.warehouses[index]}));
        this.warehouseActive = index;
        this.warehouses = this.warehouses.filter(w => w.id != state.selectedWarehouse.id);
        return;
      }
      //If a warehouse is edited, update the warehouse
      let warehouse = this.warehouses.find(w => w.id == state.selectedWarehouse.id);
      if (!warehouse) {
        return;
      }
      let index = this.warehouses.indexOf(warehouse)
      this.warehouses[index] = state.selectedWarehouse;
    });
  }

  showInventory() {
    this.inventoryActive = true;
    this.showInventoryEvent.emit(this.inventoryActive);
  }

  showDiary() {
    this.inventoryActive = false;
    this.showInventoryEvent.emit(this.inventoryActive);
  }

  onSelectWarehouse(index: number, warehouse: Warehouse) {
    this.warehouseActive = index;
    this.store.dispatch(setSelectedWarehouse({warehouse: warehouse}));
  }

  createWarehouse() {
    this.service.create({name: "New Warehouse"}).then(warehouse => {
      console.log("Created warehouse: " + warehouse);
      this.warehouses.push(warehouse);
      //Select the newly created warehouse, if that is the only one
      if (this.warehouses.length == 1) {
        this.warehouseActive = 0;
        this.store.dispatch(setSelectedWarehouse({warehouse: warehouse}));
      }
    });
  }

  async logout(){
    localStorage.removeItem('token');
    await this.router.navigate(['login'])
  }
}
