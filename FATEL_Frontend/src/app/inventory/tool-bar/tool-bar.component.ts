import {Component, OnInit} from '@angular/core';
import {Store} from '@ngrx/store';
import {setShowAddItemComponent} from "../states/app.states";

@Component({
  selector: 'app-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.css']
})
export class ToolBarComponent implements OnInit {

  appState = this.store.select('appState');

  name: string = 'Warehouse';

  constructor(private readonly store: Store<any>) {
  }

  ngOnInit(): void {
    this.appState.subscribe(state => {
      this.name = state.selectedWarehouse.name;
    });
  }

  openAddItemComponent() {
    this.store.dispatch(setShowAddItemComponent());
  }
}
