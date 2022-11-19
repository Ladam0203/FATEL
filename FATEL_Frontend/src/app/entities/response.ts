import {Entry} from "./entry";
import {Unit} from "./units";

export interface response {
  id: number;
  name: string;
  length?: number;
  width?: number;
  unit: Unit;
  quantity: number;
  note?: string;
  entry: Entry | undefined;
}
