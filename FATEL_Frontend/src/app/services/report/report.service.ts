import {Injectable} from '@angular/core';
import jsPDF from "jspdf";
import {RobotoRegular} from "./Roboto-Regular";
import autoTable from "jspdf-autotable";
import {Unit} from "../../entities/units";
import {Entry} from "../../entities/entry";
import {TranslateService} from "@ngx-translate/core";

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  units: typeof Unit = Unit;

  constructor(private translate: TranslateService) {
  }

  createInventoryReport(warehouseName: string, inventory: any[]) {
    const doc = this.getDoc();

    let parsedData: any[][] = [];
    inventory.forEach((item) => {
      let row: any[] = [
        item.name,
        Number(item.quantity.toFixed(3)),
        this.translate.instant(this.units[item.unit])
      ];
      parsedData.push(row);
    });

    //date in hungarian format
    let now = new Date();
    let huDate = new Date().toLocaleDateString("hu-HU");
    let fileDate = now.getFullYear().toString() + (now.getMonth() + 1).toString() + now.getDate().toString();

    doc.text( this.translate.instant("INVENTORY") + " | " + warehouseName + " " + huDate, 20, 25);
    autoTable(doc, {
        head: [
          [this.translate.instant('REPORT.INVENTORY.NAME'),
            this.translate.instant('REPORT.INVENTORY.QUANTITY'),
            this.translate.instant('REPORT.INVENTORY.UNIT')]
        ],
        body: parsedData,
        styles: {
          font: 'Roboto-Regular',
        },
        theme: "striped",
      }
    );
    doc.save(warehouseName + fileDate + '.pdf');
  }

  createDiaryReport(warehouseName: string, entries: Entry[]) {
    const doc = this.getDoc();

    let parsedData: any[][] = [];
    entries.forEach((entry) => {
      let row: any[] = [new Date(entry.timestamp).toLocaleDateString('hu-HU') + " " + new Date(entry.timestamp).toLocaleTimeString('hu-HU').slice(0, 5), entry.itemName, Number(entry.change.toFixed(3)), Number(entry.quantityAfterChange.toFixed(3))];
      parsedData.push(row);
    });

    //date in hungarian format
    let now = new Date();
    let huDate = new Date().toLocaleDateString("hu-HU");
    let fileDate = now.getFullYear().toString() + (now.getMonth() + 1).toString() + now.getDate().toString();

    doc.text(this.translate.instant("DIARY") + " | " + warehouseName + " " + huDate, 20, 25);
    autoTable(doc, {
        head: [
          [this.translate.instant('REPORT.DIARY.TIMESTAMP'),
            this.translate.instant('REPORT.DIARY.NAME'),
            this.translate.instant('REPORT.DIARY.CHANGE'),
            this.translate.instant('REPORT.DIARY.AFTER')]
        ],
        body: parsedData,
        styles: {
          font: 'Roboto-Regular',
        },
        theme: "striped",
      }
    );
    doc.save(warehouseName + fileDate + '.pdf');
  }

  getDoc() {
    const doc = new jsPDF('p', 'pt', 'a4');

    doc.addFileToVFS('Roboto-Regular.ttf', RobotoRegular);
    doc.addFont('Roboto-Regular.ttf', 'Roboto-Regular', 'regular');

    return doc;
  }
}
