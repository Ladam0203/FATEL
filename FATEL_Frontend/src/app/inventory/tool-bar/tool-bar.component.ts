import {Component, OnInit} from '@angular/core';
import {Store} from '@ngrx/store';
import {setShowAddItemComponent} from "../states/categories.states";

@Component({
  selector: 'app-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.css']
})
export class ToolBarComponent implements OnInit {

  constructor(private readonly store: Store<any>) {
  }

  ngOnInit(): void {
  }

  buttonClick() {

  }

  openAddItemComponent() {
    this.store.dispatch(setShowAddItemComponent());
  }
}
