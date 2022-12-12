import {Component} from '@angular/core';
import {TranslateService} from "@ngx-translate/core";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent{
  title = 'FATEL_Frontend';

  constructor(private translate: TranslateService) {
    translate.setDefaultLang('en');

    switch (navigator.language) {
      case 'hu':
        translate.use('hu');
    }
  }

}
