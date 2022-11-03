import {Component, OnInit} from '@angular/core';
import {Warehouse} from "../entities/warehouse";
/*import {WarehouseService} from "../warehouse.service";*/
import {WAREHOUSES} from "../mock-objects/mock-warehouses";

@Component({
  selector: 'warehouse-navbar',
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.css']
})
export class WarehouseComponent implements OnInit {
  warehouses = WAREHOUSES;

  constructor() {
  }

  ngOnInit(): void {
    //this.getAllWarehouses();
  }

  getAllWarehouses(): void {
    //this.warehouseService.getAllWarehouses();
      /*.subscribe(warehouse => this.warehouses = warehouse);*/
  }
}
