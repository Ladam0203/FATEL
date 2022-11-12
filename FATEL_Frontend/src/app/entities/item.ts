import {Unit} from './units'

export interface Item {
  id: number;
  name: string;
  length?: number;
  width?: number;
  unit: Unit;
  quantity: number;
  note?: string;
}
