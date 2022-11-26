import {Unit} from './units'
import {Entry} from "./entry";

export interface Item {
  id: number;
  name: string;
  length?: number;
  width?: number;
  unit: Unit;
  quantity: number;
  note?: string;
  entry?: Entry;
}
