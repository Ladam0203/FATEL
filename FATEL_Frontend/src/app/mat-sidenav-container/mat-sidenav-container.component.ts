import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder} from "@angular/forms";

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

  constructor(private _formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }

  showInventory() {
    this.showInventoryEvent.emit(true);
  }

  showDiary() {
    this.showInventoryEvent.emit(false);
  }
}
