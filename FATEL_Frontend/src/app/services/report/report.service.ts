import {Injectable} from '@angular/core';
import jsPDF from "jspdf";
import {RobotoRegular} from "./Roboto-Regular";
import autoTable from "jspdf-autotable";

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor() {
  }

  createReport(header: string, columns: string[], data: any[]) {
    const doc = new jsPDF('p', 'pt', 'a4');

    doc.addFileToVFS('Roboto-Regular.ttf', RobotoRegular);
    doc.addFont('Roboto-Regular.ttf', 'Roboto-Regular', 'regular');

    autoTable(doc, {
      
        head: [['Name', 'Width', 'Length', 'Unit', 'Quantity']],
        body: data,
        styles: {
          font: 'Roboto-Light',
        },
        theme: "striped",

      }
    );
    doc.save('inventory.pdf');

  }
}
