import {Component, OnInit} from '@angular/core';
import {Store} from '@ngrx/store';
import {selectShowAddItemComponentValue, setShowAddItemComponent} from "../states/add-item.actions";

@Component({
  selector: 'app-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.css']
})
export class ToolBarComponent implements OnInit {

  showAddItemComponent = this.store.select(selectShowAddItemComponentValue);

  constructor(private readonly store: Store<any>) {
  }

  ngOnInit(): void {
  }

  buttonClick() {

  }

  openAddItemComponent() {
    this.store.dispatch(setShowAddItemComponent({value: true}));
  }
}
