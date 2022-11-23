import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'FATEL_Frontend';

  showInventory: boolean = true;

  constructor() {}

  onShowInventory(value: boolean) {
    this.showInventory = value;
  }
}
