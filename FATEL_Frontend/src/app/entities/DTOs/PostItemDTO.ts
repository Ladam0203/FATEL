import {Unit} from "../units";

export interface PostItemDTO {
  warehouseId: number;
  name: string;
  length?: number | null;
  width?: number | null;
  unit: Unit;
  quantity: number;
  note?: string | null;
}
