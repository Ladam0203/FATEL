import { Injectable } from '@angular/core';
import jsPDF from "jspdf";
import {RobotoRegular} from "./Roboto-Regular";
import autoTable from "jspdf-autotable";

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor() { }

  createReport(header: string, fields: string[], data: any[]) {
    const doc = new jsPDF('p', 'pt', 'a4');

    doc.addFileToVFS('Roboto-Regular.ttf', RobotoRegular);
    doc.addFont('Roboto-Regular.ttf', 'Roboto-Regular', 'regular');

    let parsedData: any[][] = [];
    data.forEach((item) => {
      let row: any[] = [];
      fields.forEach((field) => {
      row.push(item[field]);
      });
      parsedData.push(row);
    });
    //TODO: Add header
    autoTable(doc, {
        head: [fields],
        body: data,
        styles: {
          font: 'Roboto-Light',
        },
        theme: "plain",
      }
    );
    doc.save('inventory.pdf');
  }
}
