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

    let lowercaseFields = fields.map(f => f.toLowerCase());

    let parsedData: any[][] = [];
    data.forEach((item) => {
      let row: any[] = [];
      lowercaseFields.forEach((lF) => {
      row.push(item[lF]);
      });
      parsedData.push(row);
    });
    console.log(parsedData);
    //TODO: Add header
    autoTable(doc, {
        head: [fields],
        body: parsedData,
        styles: {
          font: 'Roboto-Regular',
        },
        theme: "plain",
      }
    );
    doc.save(header+'.pdf');
  }
}
