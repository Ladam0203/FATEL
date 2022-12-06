import {Injectable} from '@angular/core';
import jsPDF from "jspdf";
import {RobotoRegular} from "./Roboto-Regular";
import autoTable, {CellHook, CellHookData} from "jspdf-autotable";
import {Unit} from "../../entities/units";
import {Entry} from "../../entities/entry";
import {TranslateService} from "@ngx-translate/core";
import {Category} from "../../entities/category";
import {Item} from "../../entities/item";
import {RobotoBold} from "./Roboto-Bold";

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  units: typeof Unit = Unit;

  categories: Category[] = [];

  constructor(private translate: TranslateService) {
  }

  createInventoryReport(warehouseName: string, inventory: any[]) {
    const doc = this.getDoc();

    let parsedData: any[][] = [];
    inventory.sort((a, b) => a.name.localeCompare(b.name));
    inventory.forEach((item) => {
      let row: any[] = [
        item.name,
        Number(item.quantity.toFixed(3)),
        this.translate.instant(this.units[item.unit])
      ];
      parsedData.push(row);
    });
    //Adding the categories
    this.categories = [];
    for (const item of inventory) {
      let category = this.categories.find(c => c.name === item.name && c.unit === item.unit);
      if (category == null) {
        category = new Category(item.name, item.unit);
        this.categories.push(category);
      }
      category.items.push(item);
    }

    function getTotalQuantity(items: Item[]) {
      return items.reduce((acc, item) => {
        return acc + item.quantity * (item.length ?? 1) * (item.width ?? 1);
      }, 0)
    }

    this.categories.sort((a, b) => a.name.localeCompare(b.name));

    let parsedCategoryData: any[] [] = [];
    this.categories.forEach((category) => {
      let row = [category.name, Number(getTotalQuantity(category.items).toFixed(3)), this.translate.instant(this.units[category.unit])];
      parsedCategoryData.push(row);
      if (category.unit !== this.units.Piece) {
        category.items.forEach((item) => {
          let row;
          if (item.width != null) {
            row = [("    " + item.width + "m" + " * " + item.length + "m"), item.quantity, this.translate.instant(this.units[2])];
          } else {
            row = [("    " + item.length + "m"), item.quantity, this.translate.instant(this.units[2])];

          }
          parsedCategoryData.push(row);
        })
      }
    })

    //date in hungarian format
    let now = new Date();
    let huDate = new Date().toLocaleDateString("hu-HU");
    let fileDate = now.getFullYear().toString() + (now.getMonth() + 1).toString() + now.getDate().toString();

    doc.text(this.translate.instant("INVENTORY") + " | " + warehouseName + " " + huDate, 20, 25);
    autoTable(doc, {
        head: [
          [this.translate.instant('REPORT.INVENTORY.NAME'),
            this.translate.instant('REPORT.INVENTORY.QUANTITY'),
            this.translate.instant('REPORT.INVENTORY.UNIT')]
        ],
        body: parsedCategoryData,

        didParseCell: function (cell: CellHookData) {
          console.log(cell.row.raw.toString())
          if (!cell.row.raw.toString().includes('   ')) {
            cell.cell.styles.font = 'Roboto-Bold';
            cell.cell.styles.fontSize = 11;
          }
        },
        styles: {
          font: 'Roboto-Regular',
          fontSize: 10,
        },
        theme: "striped",
      }
    );
    doc.save(warehouseName + fileDate + '.pdf');
  }

  createDiaryReport(warehouseName: string,
                    entries: Entry[]) {
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
    doc.addFileToVFS('Roboto-Bold.ttf', RobotoBold);
    doc.addFont('Roboto-Bold.ttf', 'Roboto-Bold', 'bold');

    return doc;
  }
}
