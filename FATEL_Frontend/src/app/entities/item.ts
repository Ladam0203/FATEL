import {Unit} from './units'

export interface Item {
  Id: number;
  Name: string;
  Width?: number;
  Length?: number;
  Unit: Unit;
  Quantity: number;
  Note?: string;
}
