import { Injectable } from '@angular/core';
import jsPDF from "jspdf";
import {RobotoRegular} from "./Roboto-Regular";

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor() { }

  createReport(header: string, columns: string[], data: any[]) {
    const doc = new jsPDF('p', 'pt', 'a4');

    doc.addFileToVFS('Roboto-Regular.ttf', RobotoRegular);
    doc.addFont('Roboto-Regular.ttf', 'Roboto-Regular', 'regular');

    /*
    let data: any[][] = [];
    this.warehouse.inventory.forEach((item: Item) => {
      data.push([item.name, item.width, item.length, item.unit, item.quantity]);
    });
    autoTable(pdf, {
        head: [['Name', 'Width', 'Length', 'Unit', 'Quantity']],
        body: data,
        styles: {
          font: 'Roboto-Light',
        },
        theme: "striped",
      }
    );
    pdf.save('inventory.pdf');
    */
  }
}
