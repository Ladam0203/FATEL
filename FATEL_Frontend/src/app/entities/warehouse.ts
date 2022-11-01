import {Item} from "./item";
import {Entry} from "./entry";

export interface Warehouse {
  Id: number;
  Name: string;
  Inventory: Item[];
  Diary: Entry[];
}
