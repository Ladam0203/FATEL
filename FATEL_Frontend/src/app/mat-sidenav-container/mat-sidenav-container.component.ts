import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {Store} from "@ngrx/store";
import {Warehouse} from "../entities/warehouse";
import {WarehouseService} from "../services/warehouse.service";

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

  categoriesState = this.store.select('categoriesState');

  //TODO: Fetch warehouses from service
  warehouses: Warehouse[] = [];

  constructor(private _formBuilder: FormBuilder, private service: WarehouseService, private readonly store: Store<any>) {
  }

  ngOnInit(): void {
  }

  showInventory() {
    this.showInventoryEvent.emit(true);
  }

  showDiary() {
    this.showInventoryEvent.emit(false);
  }
}
