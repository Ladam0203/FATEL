import {Component, OnInit} from '@angular/core';
import {Store} from "@ngrx/store";
import {Warehouse} from "./entities/warehouse";
import {TranslateService} from "@ngx-translate/core";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'FATEL_Frontend';

  showInventory: boolean = true;

  selectedWarehouse: Warehouse | undefined;

  constructor(private readonly store: Store<any>, private translate: TranslateService) {
    translate.setDefaultLang('hu');
    translate.use('hu');
  }

  ngOnInit(): void {
    this.store.select('appState').subscribe(state => {
      this.selectedWarehouse = state.selectedWarehouse;
    });
  }

  onShowInventory(value: boolean) {
    this.showInventory = value;
  }
}
