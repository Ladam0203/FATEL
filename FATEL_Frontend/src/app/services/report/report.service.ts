import { Injectable } from '@angular/core';
import jsPDF from "jspdf";
import {RobotoRegular} from "./Roboto-Regular";
import autoTable from "jspdf-autotable";
import {Unit} from "../../entities/units";

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  units: typeof Unit = Unit;

  constructor() { }

  createReport(title: string, subtitle: string, fields: string[], data: any[]) {
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
        }
        else {
          row.push(item[lF]);
        }
      });
      parsedData.push(row);
    });
    console.log(parsedData);

    //date in hungarian format
    let now = new Date();
    let huDate = new Date().toLocaleDateString("hu-HU");
    let fileDate = now.getFullYear().toString() + (now.getMonth() + 1).toString() + now.getDate().toString();

    doc.text(subtitle + " | " + title + " - " + huDate, 20, 25);
    autoTable(doc, {
        head: [fields],
        body: parsedData,
        styles: {
          font: 'Roboto-Regular',
        },
        theme: "plain",
      }
    );
    doc.save(title +  fileDate + '.pdf');
  }
}
