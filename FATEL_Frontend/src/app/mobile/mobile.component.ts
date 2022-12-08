import {Component, OnInit} from '@angular/core';
import {Warehouse} from "../entities/warehouse";
import {Store} from "@ngrx/store";
import {Router} from "@angular/router";
import {WarehouseService} from "../services/warehouse.service";
import {setSelectedWarehouse} from "../states/app.states";

@Component({
  selector: 'app-mobile',
  templateUrl: './mobile.component.html',
  styleUrls: ['./mobile.component.css']
})
export class MobileComponent implements OnInit {

  warehouses: Warehouse[] = [];

  constructor(private readonly store: Store<any>,
              private router: Router,
              private service: WarehouseService) {
  }

  ngOnInit(): void {
    this.service.readAll().then(warehouses => {
      this.warehouses = warehouses;
    });
  }

  onSelectWarehouse(i: number, warehouse: Warehouse) {
    this.store.dispatch(setSelectedWarehouse({warehouse: warehouse}));
    this.router.navigate(['./mobile/inventory']);
  }
}
