import { Component } from '@angular/core';
import {FormBuilder} from "@angular/forms";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'FATEL_Frontend';

  options = this._formBuilder.group({
    bottom: 0,
    fixed: true,
    top: 0,
  });

  constructor(private _formBuilder: FormBuilder) {}
}
