import {Injectable} from '@angular/core';
import jsPDF from "jspdf";
import {RobotoRegular} from "./Roboto-Regular";
import autoTable from "jspdf-autotable";
import {Unit} from "../../entities/units";
import {Entry} from "../../entities/entry";

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  units: typeof Unit = Unit;

  constructor() {
  }

  createReport(header: string, fields: string[], data: any[]) {
    const doc = new jsPDF('p', 'pt', 'a4');

    doc.addFileToVFS('Roboto-Regular.ttf', RobotoRegular);
    doc.addFont('Roboto-Regular.ttf', 'Roboto-Regular', 'regular');

    let lowercaseFields = fields.map(f => f.toLowerCase());

    let parsedData: any[][] = [];
    data.forEach((item) => {
      let row: any[] = [];
      lowercaseFields.forEach((lF) => {
        if (lF === 'unit') {
          row.push(this.units[item[lF]]);
        } else {
          row.push(item[lF]);
        }
      });
      parsedData.push(row);
    });
    console.log(parsedData);

    doc.text(header + " " + new Date().toLocaleDateString(), 20, 25);
    autoTable(doc, {
        head: [fields],
        body: parsedData,
        styles: {
          font: 'Roboto-Regular',
        },
        theme: "plain",
      }
    );
    doc.save(header + '.pdf');
  }

  createDiaryReport(warehouseName: string, data: Entry[]) {
    const doc = new jsPDF('p', 'pt', 'a4');

    doc.addFileToVFS('Roboto-Regular.ttf', RobotoRegular);
    doc.addFont('Roboto-Regular.ttf', 'Roboto-Regular', 'regular');

    let now = new Date();
    let huDate = new Date().toLocaleDateString("hu-HU");
    let fileDate = now.getFullYear().toString() + (now.getMonth() + 1).toString() + now.getDate().toString();

    let parsedData: any[][] = [];
    data.forEach((entry) => {
      let row: any[] = [new Date(entry.timestamp).toLocaleDateString('hu-HU') + " " + new Date(entry.timestamp).toLocaleTimeString('hu-HU').slice(0, 5), entry.itemName, Number(entry.change.toFixed(3)), Number(entry.quantityAfterChange.toFixed(3))];
      parsedData.push(row);
    });
    console.log(parsedData);


    doc.text("Diary " + warehouseName + " " + huDate, 20, 25);
    autoTable(doc, {
        head: [['Timestamp', 'Item Name', 'Change', 'After'],],
        body: parsedData,
        styles: {
          font: 'Roboto-Regular',
        },
        theme: "striped",
      }
    );
    doc.save(warehouseName + fileDate + '.pdf');
  }
}
