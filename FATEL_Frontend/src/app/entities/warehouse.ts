import {Item} from "./item";
import {Entry} from "./entry";

export interface Warehouse {
  id: number;
  name: string;
  inventory: Item[];
  diary: Entry[];
}
