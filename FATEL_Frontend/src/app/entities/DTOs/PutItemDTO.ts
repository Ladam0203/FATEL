import {Unit} from "../units";

export interface PutItemDTO {
  id: number,
  name: string,
  length?: number | null;
  width?: number | null;
  unit: Unit;
  note?: string | null;
}
