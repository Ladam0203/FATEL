import {Item} from "./item";
import {Unit, UnitUtil} from "./units";

export class Category {
  public name: string;
  public unit: Unit;
  public items: Item[] = [];

  constructor(name: string, unit: Unit) {
    this.name = name;
    this.unit = unit;
  }

  getDescription(): string {
    let totalQuantity = 0;
    for (const item of this.items) {
      totalQuantity += item.quantity * (item.length ?? 1)  * (item.width ?? 1);
    }
    return "" + totalQuantity + UnitUtil.abbreviations(this.unit);
  }

export interface Category {
  name: string;
  items: Item[];
}
