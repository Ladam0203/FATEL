import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {MatSidenavContainerComponent} from "../mat-sidenav-container/mat-sidenav-container.component";
import {FormBuilder} from "@angular/forms";
import {WarehouseService} from "../services/warehouse.service";
import {Store} from "@ngrx/store";
import {Router} from "@angular/router";
import {Warehouse} from "../entities/warehouse";
import {setSelectedWarehouse} from "../states/app.states";

@Component({
  selector: 'app-mobile-nav',
  templateUrl: './mobile-nav.component.html',
  styleUrls: ['./mobile-nav.component.css']
})
export class MobileNavComponent implements OnInit {

  @Output() showInventoryEvent = new EventEmitter<boolean>();

  options = this._formBuilder.group({
    bottom: 0,
    fixed: true,
    top: 0,
  });

  appState = this.store.select('appState');

  inventoryActive: boolean = true;
  warehouseActive: number = 0;

  warehouses: Warehouse[] = [];

  constructor(private _formBuilder: FormBuilder, private service: WarehouseService, private readonly store: Store<any>, private router: Router) {
  }

  ngOnInit(): void {
    this.service.readAll().then(warehouses => {
      this.warehouses = warehouses;

      let firstWarehouse = this.warehouses[0];
      if (firstWarehouse)
        this.store.dispatch(setSelectedWarehouse({warehouse: firstWarehouse}));
    });

    this.appState.subscribe(state => {
      if (!state.selectedWarehouse) {
        return;
      }
      if (state.selectedWarehouse.name == null) {
        let index = this.warehouses.findIndex(warehouse => warehouse.id == state.selectedWarehouse.id);
        this.store.dispatch(setSelectedWarehouse({warehouse: this.warehouses[index - 1]}));
        this.warehouses = this.warehouses.filter(w => w.id != state.selectedWarehouse.id);
        return;
      }
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

  onSelectWarehouse(index: number, warehouse: Warehouse) {
    this.warehouseActive = index;
    this.store.dispatch(setSelectedWarehouse({warehouse: warehouse}));
  }

  async logout(){
    localStorage.removeItem('token');
    await this.router.navigate(['login'])
  }

}
