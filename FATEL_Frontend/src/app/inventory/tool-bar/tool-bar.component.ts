import {Component, OnInit} from '@angular/core';
import {Store} from '@ngrx/store';
import {setShowAddItemComponent} from "../../states/app.states";
import {ReportService} from "../../services/report/report.service";

@Component({
  selector: 'app-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.css']
})

export class ToolBarComponent implements OnInit {

  constructor(private readonly store: Store<any>, private readonly reportService: ReportService) {
  }

  ngOnInit(): void {
  }

  openAddItemComponent() {
    this.store.dispatch(setShowAddItemComponent());
  }

  exportPDF() {
    this.reportService.createReport()
  }
}
