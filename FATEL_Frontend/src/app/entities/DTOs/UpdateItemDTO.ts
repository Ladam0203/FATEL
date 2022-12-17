import {Unit} from "../units";

export interface UpdateItemDTO {
  id: number,
  name: string,
  length?: number | null;
  width?: number | null;
  unit: Unit;
  note?: string | null;
}
