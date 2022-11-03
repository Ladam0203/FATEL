import {Unit} from './units'

export interface Item {
  id: number;
  name: string;
  width?: number;
  length?: number;
  unit: Unit;
  quantity: number;
  note?: string;
}
